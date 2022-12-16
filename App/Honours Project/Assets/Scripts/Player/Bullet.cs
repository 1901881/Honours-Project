 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;

    private int counter;

    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//sets rigid body to variable
        tr.emitting = true;

        StartCoroutine(DespawnTimer());
    }

    private void Update()
    {
        lastVelocity = rb.velocity;

     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "Bullet")
        {
            //Destroy(gameObject);
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0.0f);
            rb.drag += 0.2f;

            counter++;

            if (counter >= 3)
            {

                Destroy(gameObject);
                counter = 0;
            }
        }

    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        Destroy(gameObject);
        counter = 0;
    }
}

