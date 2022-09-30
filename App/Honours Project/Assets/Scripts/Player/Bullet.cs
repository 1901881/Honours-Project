 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;

    private int counter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//sets rigid body to variable
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Destroy(gameObject);
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0.0f);
        counter++;

        if(counter >= 3)
        {
            Destroy(gameObject);
            counter = 0;
        }
    }
}
