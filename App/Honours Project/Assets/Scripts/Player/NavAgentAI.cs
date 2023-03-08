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
    private Color originalColor;


    public int health = 3;

    public int stressResponse;
    public float stressValue;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;
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
        SpriteRend.color = Color.white;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        SpriteRend.color = originalColor;
        health--;

    }

    private void OnDrawGizmos()//Selected
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, fleeRadius);
    }

    void CalculateStress()
    {
        /*     bool healthStress = false;
             if(health <= 1 && !healthStress)
             {
                 stressValue += 10;
                 healthStress = true;
             }*/
 /*       target = GameObject.FindWithTag("Player").transform.position;
        float maxDistance = 10.0f;
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget > maxDistance)
        {
            distanceToTarget = 1;//make it null
        }*/
        //stress value/
        // x = health * distance to player * bullets near player * stress likeliness

        //maybe cap the range of distance
        //reverse health

        //stressValue = health * distanceToTarget;
        //Debug.Log(stressValue);
        //Debug.Log(distanceToTarget);

        float healthFactor = 1;
        float distanceFactor = 1;
        float bulletFactor = 1;
        float hitFactor = 1;

        //just do adds?, maybe speak to salma about this



        float stressFortitude = 0;
        float stressValue = 0;

        stressValue = healthFactor * distanceFactor * bulletFactor * hitFactor;

        if(stressValue >= stressFortitude)
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

        }

       
    }
}
