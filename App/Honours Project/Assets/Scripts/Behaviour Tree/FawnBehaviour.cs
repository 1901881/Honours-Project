using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class FawnBehaviour : Action
    {
      


        public override TaskStatus OnUpdate()
        {
            GetComponent<NavAgentAI>().FawnResponse(true);
            return TaskStatus.Success;
        }

        /*
         want to switcvh it to attack NPCs 
        bool switch over NPC script
         */
    }
}

