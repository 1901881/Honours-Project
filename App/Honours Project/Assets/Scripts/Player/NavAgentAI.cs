using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentAI : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(transform.position, new Quaternion( 0f, 0f, transform.rotation.z, transform.rotation.w));
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }
}

/*

//public NavMeshAgent agent;
//agent = GetComponent<NavMeshAgent>();
// agent.updateUpAxis = false;
//agent.updateRotation = false;

 */
