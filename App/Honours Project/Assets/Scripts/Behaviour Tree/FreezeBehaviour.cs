using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class FreezeBehaviour : Action
    {
        public float speed = 0;

        [Tooltip("The GameObject that the agent is pursuing")]
        public SharedTransform target;
        

        public override TaskStatus OnUpdate()
        {
            float test = Vector3.Magnitude(transform.position - target.Value.transform.position);

            if (test > 3)
            {
                int counter = 0;

                //loop through stress weightings
                for (int i = 0; i < GetComponent<NavAgentAI>().stressWeightings.Length; i++)
                {
                    if (GetComponent<NavAgentAI>().stressWeightings[i] != 0)
                    {
                        counter++;
                    }
                }
                if (counter >= 2)
                {
                    float savedFreezeWeighting = GetComponent<NavAgentAI>().freezeWeighting;
                    GetComponent<NavAgentAI>().freezeWeighting = 0f;

                    GetComponent<NavAgentAI>().stressResponseRunning = false;
                    GetComponent<NavAgentAI>().StressResponseCalculation();

                    return TaskStatus.Success;
                }
                else //if the other stress waitings are 0 go back to basic NPC behaviour
                {
                    GetComponent<NavAgentAI>().stressResponseRunning = false;
                    return TaskStatus.Success;
                }

            }

            // We haven't reached the target yet so keep moving towards it
            transform.position = Vector3.MoveTowards(transform.position, transform.position - target.Value.transform.position.normalized * 5, speed * Time.deltaTime);
            return TaskStatus.Running;
        }
    }
}

