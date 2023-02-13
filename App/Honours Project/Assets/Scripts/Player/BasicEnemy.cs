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

    
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 direction;

    private bool isInSightRange;
    private bool isInChaseRange;
    private bool isInAttackRange;

    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;
        target = GameObject.FindWithTag("Player").transform;
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

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
       
    }

    private void FixedUpdate()
    {
        if (isInSightRange && !isInChaseRange)
        {
            MoveCharacter(movement, speed/3);
        }
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement, speed);
        }
        if(isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
        }
        
    }

    private void MoveCharacter(Vector2 direction, float speed)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, sightRadius);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, chaseRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, attackRadius);
    }
}

/*
 made it destroy on collision with bullet
 */