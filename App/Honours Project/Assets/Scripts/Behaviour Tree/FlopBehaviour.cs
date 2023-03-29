using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class FlopBehaviour : Action
    {
        public float speed = 0;
        public float distance = 3;


        // The time to wait
        private float waitDuration;
        // The time that the task started to wait.
        private float startTime;


        /*
          generate random number 0-1
            
            if 0 - copy wait behaviour for random amount of time between a range
         */

        public override void OnStart()
        {
            // Remember the start time.
            startTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            float randomNumber = UnityEngine.Random.Range(0, 100);
            if (randomNumber <= 30)
            {
                //explode
                //need it to shake
                //then explode
                Debug.Log("Kaboom");
                GetComponent<NavAgentAI>().KillNPC();
            }
            else
            {
                //freeze
                Debug.Log("freeze waiting");
                // The task is done waiting if the time waitDuration has elapsed since the task was started.
                if (startTime + waitDuration < Time.time)
                {
                    return TaskStatus.Success;
                }
            }
            // Otherwise we are still waiting.
            return TaskStatus.Running;
        }
    }
}

