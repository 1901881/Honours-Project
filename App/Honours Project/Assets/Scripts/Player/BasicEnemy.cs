using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float sightRadius;
    public float chaseRadius;
    public float attackRadius;

    public LayerMask whatIsPlayer;

    
    private Vector3 target;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 direction;

    private bool isInSightRange;
    private bool isInChaseRange;
    private bool isInAttackRange;

    private bool isAttacking = false;
    private bool isPatrolling = false;
    private float baseSpeed;

    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private Color originalColor;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;
        target = GameObject.FindWithTag("Player").transform.position;
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        isInSightRange = Physics2D.OverlapCircle(transform.position, sightRadius, whatIsPlayer);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;

    }

    private void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        if(!isInSightRange)
        {
            
            calcualtePatrolPoint();
            isPatrolling = true;
            float distanceToTarget = Vector3.Distance(transform.position, target);
            float distanceThreshold = 1.0f;
            if (distanceToTarget >= distanceThreshold)
            {
                MoveCharacter();
            }
            else
            {
                
                timer += Time.deltaTime;
                if(timer >= 1.0)
                {
                    isPatrolling = false;
                    timer = 0;
                }
                //StartCoroutine(PatrolPause());
            }
        }
        if (isInSightRange)
        {
            target = GameObject.FindWithTag("Player").transform.position;
            MoveCharacter();
        }
        if (!isAttacking)
        {
            if (isInSightRange && !isInChaseRange && !isInAttackRange)
            {
                if (speed != baseSpeed / 2)
                {
                    speed = baseSpeed / 2;
                }
            }
            if (isInChaseRange && !isInAttackRange)
            {
                if (speed != baseSpeed)
                {
                    speed = baseSpeed;
                }
            }
            if (isInAttackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
        }
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().Hit());
            Debug.Log("hit player");
            StartCoroutine(AttackPause());
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

    IEnumerator Attack()
    {
        isAttacking = true;
        speed = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        speed = baseSpeed * 2;
        yield return new WaitForSecondsRealtime(2.2f);
        speed = 0;
        yield return new WaitForSecondsRealtime(0.2f);
        speed = baseSpeed / 2;
        isAttacking = false;
    }

    IEnumerator AttackPause()
    {
        speed = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        speed = baseSpeed/2;
    }



    private void OnDrawGizmos()//Selected
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, sightRadius);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, chaseRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, attackRadius);
    }

    private void calcualtePatrolPoint()
    {
        if(!isPatrolling)
        {
            Vector2 point = Random.insideUnitCircle.normalized * Random.Range(sightRadius-2, sightRadius);//min,max
            target = point;
        }
    }

}

/*
 
 */