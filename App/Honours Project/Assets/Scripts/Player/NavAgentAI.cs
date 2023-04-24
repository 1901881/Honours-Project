using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using static UnityEngine.Networking.UnityWebRequest;
using System.Drawing;
using System.IO;

#if UNITY_EDITOR // => Ignore from here to next endif if not in editor
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
#endif

public class NavAgentAI : MonoBehaviour
{
    [HideInInspector]
    public BehaviorTree behaviorTree;
    [HideInInspector]
    public Sprite hitSprite;

    public float fleeRadius = 2;

    [HideInInspector]
    public GameObject explosionPrefab;
    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private UnityEngine.Color originalColor;


    public enum NPCType { Brawler, ScaredyCat, freeze, flop, Dansel, Custom }
    [SerializeField] public NPCType npcType = NPCType.Custom;
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float health = 0;
    [SerializeField] private bool stressResponseEnabled = true;

    [SerializeField] public bool[] typeUpdateVariables = new bool[6];
    // index 0: brawlerTypeUpdate
    // index 1: scaredycatTypeUpdate
    // index 2: freezeTypeUpdate
    // index 3: flopTypeUpdate
    // index 4: danselTypeUpdate
    // index 5: customTypeUpdate

    private Vector3 target;

    //Bullet Calc for stress response
    public LayerMask whatIsBullet;
    public float bulletRadius;
    int previousBulletCount = 100;


    //public stress variables
    [SerializeField] private float distanceToTarget;
    [SerializeField] private int recentlyHit = 0;
    [SerializeField] private int bulletCounter = 0;
    [SerializeField] private float healthFactor = 0;
    [SerializeField] private float distance_stress;
    [SerializeField] private float bullet_stress;
    [SerializeField] private float stressFortitudeDecrease = 0;
    [SerializeField] public float stressValue;
    [SerializeField] private  int stressResponseIndex = -1;
    [SerializeField] public bool stressResponseRunning = false;

    private bool freezeResponseRunning = false;
    private bool fawnResponseRunning = false;
    private bool stressDecreased = false;

    [Header("Stress Sliders")]
    [Tooltip("Mental strength of NPC")]
    public float[] stressWeightings;

    //public stress sliders
    [Range(0.0f, 100.0f)]
    [SerializeField] private float stressFortitude;

    [Range(0.0f, 100.0f)]
    [SerializeField] private float fightWeighting;
    [Range(0.0f, 100.0f)]
    [SerializeField] private float flightWeighting;
    [Range(0.0f, 100.0f)]
    [SerializeField] public float freezeWeighting;
    [Range(0.0f, 100.0f)]
    [SerializeField] private float flopWeighting;
    [Range(0.0f, 100.0f)]
    [SerializeField] private float fawnWeighting;

    [HideInInspector]
    public ContactFilter2D contactFilter;
    Collider2D[] results = new Collider2D[10];

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;

        health = maxHealth;

        behaviorTree.SetVariableValue("stressFortitude", stressFortitude);

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(transform.position, new Quaternion( 0f, 0f, transform.rotation.z, transform.rotation.w));

        if (health <= 0)
        {
            KillNPC();
        }

        CalculateStress();
        FreezeResponse();
        StressResponseCalculation();
        CheckBullets();
     
        //set stress variables
        stressResponseRunning = ((SharedBool)behaviorTree.GetVariable("stressResponseRunning")).Value;
        freezeResponseRunning = ((SharedBool)behaviorTree.GetVariable("freezeResponseRunning")).Value;

        stressWeightings = new float[] { fightWeighting, flightWeighting, freezeWeighting, flopWeighting, fawnWeighting };
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
        }

        if(!fawnResponseRunning)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().Hit());
                Debug.Log("hit player");
                //StartCoroutine(AttackPause());
            }
        }
        else if(fawnResponseRunning)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                StartCoroutine(collision.gameObject.GetComponent<NavAgentAI>().Hit());
                Debug.Log("hit NPC");
                StartCoroutine(Hit());
            }
        }
    }

    IEnumerator Hit()
    {
        Sprite previousSprite = SpriteRend.sprite;
        SpriteRend.sprite = hitSprite;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        SpriteRend.sprite = previousSprite;
        health--;
        recentlyHit = 1;
        yield return new WaitForSecondsRealtime(10f);
        recentlyHit = 0;

    }

#if UNITY_EDITOR // => Ignore from here to next endif if not in editor
    private void OnSelectedDrawGizmos()//Selected
    {
        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, fleeRadius);

        UnityEditor.Handles.color = UnityEngine.Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, bulletRadius);
    }
#endif

    //Generates cicle around player if a bullet enter it the counter goes up
    void CheckBullets()
    {
        var bulletCollisions = Physics2D.OverlapCircle(transform.position, bulletRadius, contactFilter, results);

        if(previousBulletCount < bulletCollisions)
        {
            bulletCounter++;
            StartCoroutine(DecreaseBulletCount());
        }
        previousBulletCount = bulletCollisions;
    }

    //When the bullet counter goes up it is removed from the counter after a set time
    //This stops stacking making the stress value too high
    IEnumerator DecreaseBulletCount()
    {
        yield return new WaitForSeconds(5.0f);
        bulletCounter--;
    }

    //Uses NPC distance from player, Health of NPC, Amount of bullets near player
    //and if the player has been recently hit to calculate the stress value
    void CalculateStress()
    {
        if(player.activeSelf)//if player is alive run
        {
            target = player.transform.position;//get player position
            distanceToTarget = Vector3.Distance(transform.position, target);//calculate distance between player and NPC
            if (distanceToTarget <= 0)
            {
                distanceToTarget = 10;
            }

            healthFactor = health / maxHealth;//turns health into percentage

            distance_stress = (1 / distanceToTarget) * 30;
            bullet_stress = (bulletCounter * 0.7f) * 5;
            stressValue = (distance_stress + bullet_stress) * ((1 - (healthFactor / 2)) * (2f + recentlyHit));

            //set stress value for behaviour tree
            behaviorTree.SetVariableValue("stressValue", stressValue);
        }
    }

    public void StressResponseCalculation()
    {
        if (stressResponseEnabled) 
        {
            if (stressValue >= stressFortitude)//if stress value surpasses stress fortitude run
            {
                int[] stressCounter = new int[5];//fight, flight, freeze, flop, fawn

                int testAmount = 50;

                //Loops through stress weightings and checks it against a random number, if number is within stress weighting range add to the counter.
                for (int i = 0; i < stressWeightings.Length; i++)
                {
                    for (int x = 0; x < testAmount; x++)
                    {
                        float randomNumber = UnityEngine.Random.Range(0, 100);
                        if (randomNumber <= stressWeightings[i])
                        {
                            stressCounter[i]++;
                        }
                    }
                }

                if (!((SharedBool)behaviorTree.GetVariable("stressResponseRunning")).Value)
                {
                    //gets the stress weighting index with the biggest counter
                    stressResponseIndex = Array.IndexOf(stressCounter, stressCounter.Max());

                    //Safety Check so it goes with the only stress response if others have a waiting of 0
                    int counter = 0;
                    for (int i = 0; i < stressWeightings.Length; i++)
                    {
                        if (GetComponent<NavAgentAI>().stressWeightings[i] != 0)
                        {
                            counter++;
                        }
                    }
                    if (counter == 1)
                    {
                        stressResponseIndex = Array.IndexOf(stressWeightings, stressWeightings.Max());
                    }
                }

                //set stressResponse index value for behaviour tree.
                behaviorTree.SetVariableValue("stressResponseIndex", stressResponseIndex);
                
                //Stress fortitude of NPC decreases after first stress response
                //does not decrease if already below 10
                if (stressFortitude >= 10)
                {
                    if(!stressDecreased)
                    {
                        stressFortitude -= stressFortitudeDecrease;
                        stressDecreased = true;
                    }
                }
            }
        }
    }

    //Sets stressResponseRunning to false after set time - Used as a timer before a new stress response can be performed
    public IEnumerator ResponseWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ((SharedBool)behaviorTree.GetVariable("stressResponseRunning")).SetValue(false);

    }

    //Sets stress value to be 100 after freeze response so that another response is immediately played
    public void FreezeResponse()
    {
        if(freezeResponseRunning)
        {
            stressValue = 100; //put after calculation on update?
            behaviorTree.SetVariableValue("stressValue", stressValue);
        }
    }

    //Sets bool when fawn response played to adjust collision code
    public void FawnResponse(bool fawnResponseRunning)
    {
        this.fawnResponseRunning = fawnResponseRunning;

    }

    //Destroys NPC and plays explosion effect
    public void KillNPC()
    {
        Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }
}