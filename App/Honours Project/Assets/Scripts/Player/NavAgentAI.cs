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

public class NavAgentAI : MonoBehaviour
{
/*    public float seekSpeed;
    public float seekViewAngle;
    public float seekViewDistance;

    public float pursueSpeed;
    public float pursueViewAngle;
    public float pursueViewDistance;

    public float attackSpeed;
    public float attackViewAngle;
    public float attackViewDistance;*/

    public BehaviorTree behaviorTree;
    public float fleeRadius = 2;

    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private UnityEngine.Color originalColor;


    public float maxHealth = 3;
    public float health = 0;

    public int stressResponse;
   

    private Vector3 target;

    public LayerMask whatIsBullet;
    private bool isInBulletRange;
    public float bulletRadius;
    
    int previousBulletCount = 100;
    float timer = 0;


    //public stress variables
    public float distanceToTarget;
    public int recentlyHit = 0;
    public int bulletCounter = 0;
    public float healthFactor = 0;
    public float distance_stress;
    public float bullet_stress;
    public float stressValue;
    public  int stressResponseIndex = -1;
    public bool stressResponseRunning = false;

    

    [Range(0.0f, 100.0f)]
    public float stressFortitude = 60;

    [Range(0.0f, 100.0f)]
    public float fightWeighting = 0;

    [Range(0.0f, 100.0f)]
    public float flightWeighting = 0;

    [Range(0.0f, 100.0f)]
    public float freezeWeighting = 0;

    public float[] stressWeightings;

    public ContactFilter2D contactFilter;
    Collider2D[] results = new Collider2D[10];

    // Start is called before the first frame update
    void Start()
    {
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;

        health = maxHealth;

        behaviorTree.SetVariableValue("stressFortitude", stressFortitude);
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(transform.position, new Quaternion( 0f, 0f, transform.rotation.z, transform.rotation.w));
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        CalculateStress();
        CheckBullets();

        //Debug.Log(recentlyHit);
        behaviorTree.SetVariableValue("stressResponseRunning", stressResponseRunning);

        stressWeightings = new float[] { fightWeighting, flightWeighting, freezeWeighting };
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
        }
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().Hit());
            Debug.Log("hit player");
            //StartCoroutine(AttackPause());
        }
    }

    IEnumerator Hit()
    {
        SpriteRend.color = UnityEngine.Color.white;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        SpriteRend.color = originalColor;
        health--;
        recentlyHit = 1;
        yield return new WaitForSecondsRealtime(10f);
        recentlyHit = 0;

    }

    private void OnDrawGizmos()//Selected
    {
        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, fleeRadius);

        UnityEditor.Handles.color = UnityEngine.Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, bulletRadius);
    }

    void CheckBullets()
    {
        var bulletCollisions = Physics2D.OverlapCircle(transform.position, bulletRadius, contactFilter, results);

        if(previousBulletCount < bulletCollisions)
        {
            bulletCounter++;
            StartCoroutine(DecreaseBulletCount());
        }
        previousBulletCount = bulletCollisions;


        //Debug.Log("Bullets near bot: " + bulletCounter);
    }

    IEnumerator DecreaseBulletCount()
    {
        yield return new WaitForSeconds(5.0f);
        bulletCounter--;
    }

    void CalculateStress()
    {
        //Player to distance
        target = GameObject.FindWithTag("Player").transform.position;
        distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget <= 0)
        {
            distanceToTarget = 10;
        }


        healthFactor = health / maxHealth;

        distance_stress = (1 / distanceToTarget) * 30;
        bullet_stress = (bulletCounter * 0.7f) * 5;
        stressValue = (distance_stress + bullet_stress) * ((1 - (healthFactor/2)) * (2f + recentlyHit));

        //set stress value for behaviour tree
        behaviorTree.SetVariableValue("stressValue", stressValue);

        StressResponseCalculation();
    }

    public void StressResponseCalculation()
    {
        if (stressValue >= stressFortitude)
        {
            
            int[] stressCounter = new int[3];//fight, flight, freeze

            int testAmount = 5;

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

            if (!stressResponseRunning)
            {
                //check which one is bigger, then return the case
                stressResponseIndex = Array.IndexOf(stressCounter, stressCounter.Max());
                //set stressResponse index value for behaviour tree.
                behaviorTree.SetVariableValue("stressResponseIndex", stressResponseIndex);
                stressResponseRunning = true;
            }
        }
    }
}
/*switch (stressResponseIndex)
            {
                case 0:
                    Debug.Log("fight");
                    break;
                case 1:
                    Debug.Log("flight");
                    break;
                case 2:
                    Debug.Log("freeze");
                    break;
            }*/

/*         timer += Time.deltaTime;
         if (timer > 10)
         {
             stressResponseRunning = false;
             timer = 0;
         }*/