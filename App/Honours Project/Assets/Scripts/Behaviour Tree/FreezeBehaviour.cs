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
                return TaskStatus.Success;
            }

            // We haven't reached the target yet so keep moving towards it
            transform.position = Vector3.MoveTowards(transform.position, transform.position - target.Value.transform.position.normalized * 5, speed * Time.deltaTime);
            return TaskStatus.Running;
        }
    }
}

