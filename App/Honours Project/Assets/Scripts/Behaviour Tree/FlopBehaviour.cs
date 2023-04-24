using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Freezes NPC for set time, has a chance to explode")]
    public class FlopBehaviour : NavMeshMovement
    {
        // The time to wait
        public float waitDuration;
        public float selfDestructProbability = 25;

        // The time that the task started to wait.
        private float startTime;

        private float randomNumber;

        private bool flopStressResponseRunning = true;
        private bool timerRunning = true;

        public override void OnStart()
        {
            startTime = Time.time;
            randomNumber = UnityEngine.Random.Range(0, 100);
            Stop(); //stops NPC from moving
        }

        public override TaskStatus OnUpdate()
        {
            if(!flopStressResponseRunning || timerRunning)
            {
                //Self Destruct check
                if (randomNumber <= selfDestructProbability)
                {
                    flopStressResponseRunning = true;
                    Debug.Log("Kaboom");
                    GetComponent<NavAgentAI>().KillNPC();
                }
                else
                {
                    //Freeze for set time
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

