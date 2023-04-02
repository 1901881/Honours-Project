using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DespawnTimer());
    }
    IEnumerator DespawnTimer()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(gameObject);
    }
}
