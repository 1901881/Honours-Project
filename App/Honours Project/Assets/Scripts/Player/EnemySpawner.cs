using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject NPCPrefab;

    //public Vector3 position = new Vector3(0f, 0f, 0f);
    public Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject NPC = Instantiate(NPCPrefab, this.gameObject.transform.position, rotation);
        }
    }
}
