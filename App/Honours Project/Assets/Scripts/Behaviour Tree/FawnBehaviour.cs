using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Makes NPC receive and give damage to other NPCs")]
    public class FawnBehaviour : Action
    {
        public override TaskStatus OnUpdate()
        {
            GetComponent<NavAgentAI>().FawnResponse(true);
            return TaskStatus.Success;
        }
    }
}

