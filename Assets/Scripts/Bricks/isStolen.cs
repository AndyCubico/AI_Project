using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Treasure Stolen?")]
[Help("Checks whether Treasure is stolen")]

public class isStolen : ConditionBase
{
    [InParam("Treasure")]
    public GameObject targetGameobject;

    public override bool Check()
    {
        return !targetGameobject.activeInHierarchy;
    }
}
