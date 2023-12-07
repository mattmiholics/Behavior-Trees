using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Navigation/MoveAwayFromGameObject")]
    [Help("Moves the game object away from a given target by using a NavMeshAgent")]
    public class MoveAwayFromGameObject : GOAction
    {
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        private UnityEngine.AI.NavMeshAgent navAgent;

        private Transform targetTransform;

        public override void OnStart()
        {
            if (target == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }
            targetTransform = target.transform;

            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }

            // Calculate the position opposite to the target
            Vector3 oppositePosition = navAgent.transform.position + (navAgent.transform.position - targetTransform.position).normalized * 10f;

            navAgent.SetDestination(oppositePosition);

#if UNITY_5_6_OR_NEWER
            navAgent.isStopped = false;
#else
            navAgent.Resume();
#endif
        }

        public override TaskStatus OnUpdate()
        {
            if (target == null)
                return TaskStatus.FAILED;

            // Check if the agent has reached the opposite position
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
                return TaskStatus.COMPLETED;

            // Update the destination to move away from the target
            Vector3 oppositePosition = navAgent.transform.position + (navAgent.transform.position - targetTransform.position).normalized * 10f;
            navAgent.SetDestination(oppositePosition);

            return TaskStatus.RUNNING;
        }

        public override void OnAbort()
        {
#if UNITY_5_6_OR_NEWER
            if (navAgent != null)
                navAgent.isStopped = true;
#else
            if (navAgent != null)
                navAgent.Stop();
#endif
        }
    }
}