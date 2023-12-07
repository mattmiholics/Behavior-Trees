using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("Perception/IsHealthLow")]
    [Help("Checks enemies health to determine if it is low")]

   
    public class IsHealthLow : GOCondition
    {
        ///<value>Input Target Parameter to check the angle.</value>
        [InParam("enemy")]
        [Help("Enemy to get the health from")]
        public Enemy enemy;
        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        ///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;
        public override bool Check()
        {
            if (enemy.health <= 2 && (gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance)
                return true;
            else
                return false;
        }
    }
}
