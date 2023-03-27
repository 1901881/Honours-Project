using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("")]
public class ResponseReset : Action
{

    public float waitTime = 5;

    //public SharedBool stressResponseRunning;



    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        StartCoroutine(gameObject.GetComponent<NavAgentAI>().ResponseWait(waitTime));
        return TaskStatus.Success;
    }
}
