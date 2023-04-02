using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class FlopBehaviour : NavMeshMovement
    {
        // The time to wait
        public float waitDuration;
        public float selfDestructProbability = 10;

        // The time that the task started to wait.
        private float startTime;

        private float randomNumber;

        private bool flopStressResponseRunning = true;
        private bool timerRunning = true;
        

        /*
          generate random number 0-1
            
            if 0 - copy wait behaviour for random amount of time between a range
         */

        public override void OnStart()
        {
            // Remember the start time.
            startTime = Time.time;
            randomNumber = UnityEngine.Random.Range(0, 100);
            //need it to stop moving
            Stop();
        }

        public override TaskStatus OnUpdate()
        {
            if(!flopStressResponseRunning || timerRunning)
            {
                if (randomNumber <= selfDestructProbability)
                {
                    //explode
                    //need it to shake
                    //then explode
                    flopStressResponseRunning = true;
                    Debug.Log("Kaboom");
                    GetComponent<NavAgentAI>().KillNPC();
                }
                else
                {
                    //freeze
                    flopStressResponseRunning = true;
                    timerRunning = true;
                    Debug.Log("freeze waiting");
                    // The task is done waiting if the time waitDuration has elapsed since the task was started.
                    if (startTime + waitDuration < Time.time)
                    {
                        Debug.Log("freeze finished");
                        return TaskStatus.Success;
                    }
                }
            }
            // Otherwise we are still waiting.
            return TaskStatus.Running;
        }
    }
}

