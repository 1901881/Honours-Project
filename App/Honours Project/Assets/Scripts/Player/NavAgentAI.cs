using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;

public class NavAgentAI : MonoBehaviour
{
    public float seekSpeed;
    public float seekViewAngle;
    public float seekViewDistance;

    public float pursueSpeed;
    public float pursueViewAngle;
    public float pursueViewDistance;

    public float attackSpeed;
    public float attackViewAngle;
    public float attackViewDistance;

    public BehaviorTree behaviorTree;


    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private Color originalColor;


    public int health = 3;



    // Start is called before the first frame update
    void Start()
    {
        /*        behaviorTree.SetVariableValue("seekSpeed", seekSpeed);
                behaviorTree.SetVariableValue("seekViewAngle", seekViewAngle);
                behaviorTree.SetVariableValue("seekViewDistance", seekViewDistance);

                behaviorTree.SetVariableValue("pursueSpeed", pursueSpeed);
                behaviorTree.SetVariableValue("pursueViewAngle", pursueViewAngle);
                behaviorTree.SetVariableValue("pursueViewDistance", pursueViewDistance);

                behaviorTree.SetVariableValue("attackSpeed", attackSpeed);
                behaviorTree.SetVariableValue("attackViewAngle", attackViewAngle);
                behaviorTree.SetVariableValue("attackViewDistance", attackViewDistance);*/

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
}

/*

//public NavMeshAgent agent;
//agent = GetComponent<NavMeshAgent>();
// agent.updateUpAxis = false;
//agent.updateRotation = false;

 */
