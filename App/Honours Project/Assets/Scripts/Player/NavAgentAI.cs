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
    public float stressValue;


    public ContactFilter2D contactFilter;
    Collider2D[] results = new Collider2D[10];

    // Start is called before the first frame update
    void Start()
    {
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;

        health = maxHealth;
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
        }
        previousBulletCount = bulletCollisions;

        if(bulletCounter > 0)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                bulletCounter = 0;
                timer = 0;
            }
        }
        //Debug.Log("Bullets near bot: " + bulletCounter);
    }

    void CalculateStress()
    {
        /*     bool healthStress = false;
             if(health <= 1 && !healthStress)
             {
                 stressValue += 10;
                 healthStress = true;
             }*/
        target = GameObject.FindWithTag("Player").transform.position;
        //float maxDistance = 10.0f;
        distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget <= 0)
        {
            distanceToTarget = 10;
        }
        //stress value/
        // x = health * distance to player * bullets near player * stress likeliness

        //maybe cap the range of distance
        //reverse health

        //stressValue = health * distanceToTarget;
        //Debug.Log(stressValue);
        //Debug.Log(distanceToTarget);


        /*        float distanceFactor = 1;
                float bulletFactor = 1;
                float hitFactor = 1;*/

        //just do adds?, maybe speak to salma about this
        //for now lets not focus on the equation
        //and just aim to get the results first 
        //then make the equation after help

        //health can get that
        //distance - complete

        //bullets
        //hit factor

        //make health percentage - current health / maximum health
        //change recently hit to a binary value 0 and 1

        healthFactor = health / maxHealth;

        if (stressValue == 0)
        {
            Debug.Log("stress zero");
        }

        //stressValue = (bulletCounter / distanceToTarget) * (1 - healthFactor) * (1 + recentlyHit);
        //stressValue = ((bulletCounter / (10 * distanceToTarget)) * 50) * (1 - (healthFactor/100)) * (1 + recentlyHit);
        //stressValue = ((bulletCounter / (distanceToTarget ** 2)) * 100) * (1 - (healthFactor/10)) * (1 + recentlyHit);



        //stress = ((bullet_amount / (10 * distance)) * 50) * (1 - (health_percentage / 100)) * (1 + recently_hit)
        //stress = ((bullet_amount / (10 * distance)) * 100) * (1 - (health_percentage / 10)) * (1 + recently_hit) + 50
        //stress = ((bullet_amount / (distance ** 2)) * 100) * (1 - (health_percentage / 10)) * (1 + recently_hit) + 50

        //float stressFortitude = 0;


        //stressValue = healthFactor * distanceFactor * bulletFactor * hitFactor;

        //Debug.Log("Stress Value: " + stressValue);


        /*        if(stressValue >= stressFortitude)
                {
                    float fightWeighting = 0;
                    float flightWeighting = 0;
                    float freezeWeighting = 0;

                    float[] stressWeightings = { fightWeighting, flightWeighting, freezeWeighting};
                    int[] stressCounter = new int[3];//fight, flight, freeze

                    int testAmount = 5;

                    for (int i = 0; i < stressWeightings.Length; i++)
                    {
                        for (int x = 0; x < testAmount; x++)
                        {
                            float randomNumber = 0; //1-100
                            if (randomNumber <= stressWeightings[i])
                            {
                                stressCounter[i]++;
                            }
                        }
                    }

                    //check which one is bigger, then return the case
                    int maxIndex = Array.IndexOf(stressCounter, stressCounter.Max());

                    switch(maxIndex)
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
                    }

                }*/


    }
}
