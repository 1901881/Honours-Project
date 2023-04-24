using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    //The freeze behaviour makes the NPC jolt back from the player
    //then selects another stress response to perform.
    public class FreezeBehaviour : Action
    {
        //Jolt back variables
        public float speed = 0;
        public float distance = 3;

        [Tooltip("The GameObject that the agent is evading")]
        public SharedGameObject target;


        public override TaskStatus OnUpdate()
        {
            float NPCPosition = Vector3.Magnitude(transform.position - target.Value.transform.position);

            if (NPCPosition > distance)
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
                if (counter >= 2)//if theres another stress response weighting with a value higher than 0 rerun the stress response calculation
                {
                    float savedFreezeWeighting = GetComponent<NavAgentAI>().freezeWeighting;
                    GetComponent<NavAgentAI>().freezeWeighting = 0f;//set to 0 so a different stress response plays

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

