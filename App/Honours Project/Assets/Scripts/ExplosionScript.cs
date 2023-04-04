using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float explosionForce, radius;

    private void Start()
    {
        ExplosionKnockback();
        StartCoroutine(DespawnTimer());
    }
    IEnumerator DespawnTimer()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(gameObject);
    }

    private void ExplosionKnockback()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 distance = collider.transform.position - transform.position;
                if(distance.magnitude > 0)
                {
                    float force = explosionForce/distance.magnitude;
                    rb.AddForce(distance.normalized * force);
                }
            }
        }
    }

    private void OnDrawGizmos()//Selected
    {
        UnityEditor.Handles.color = UnityEngine.Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, radius);
    }
}
