using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance.
    /// </summary>
    [Condition("Perception/IsTargetCloseGuard")]
    [Help("Checks whether a target is close depending on a given distance")]
    public class IsTargetCloseGuard : GOCondition
    {
        ///<value>Input Target Parameter to to check the distance.</value>
        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        ///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;

        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
        {
            if ((gameObject.transform.position - target.transform.position).sqrMagnitude < zombieManager.myManager.distanceToKill * zombieManager.myManager.distanceToKill)
            {
                zombieManager.myManager.target.SetActive(false);
                Debug.Log(target.activeInHierarchy);
                zombieManager.myManager.zombieState = state.WANDER;
                return false;
            }

            else if (!zombieManager.myManager.target.activeInHierarchy)
            {
                return false;
            }

            if ((gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance)
            {
                zombieManager.myManager.zombieState = state.CHASE;
            }

            else
            {
                zombieManager.myManager.zombieState = state.WANDER;
            }

            return (gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance;
        }
    }
}