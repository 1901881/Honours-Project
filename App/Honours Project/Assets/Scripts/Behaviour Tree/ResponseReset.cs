using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("Sets stressResponseRunning to false after set time - Used as a timer before a new stress response can be performed")]
public class ResponseReset : Action
{
    public SharedFloat waitTime = 5;

    public override TaskStatus OnUpdate()
    {
        StartCoroutine(gameObject.GetComponent<NavAgentAI>().ResponseWait(waitTime.Value));
        return TaskStatus.Success;
    }
}
