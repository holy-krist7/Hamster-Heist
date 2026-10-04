using System;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is Target In Line Of Sight", story: "If [Target] [comparison] [Self] Line of Sight", category: "Conditions", id: "3cbeebd7d7f01f85d41c002d04475fac")]
public partial class IsTargetInLineOfSightCondition : Condition
{
    public enum C
    {
        IS,
        IS_NOT,
    }

    [SerializeReference] public BlackboardVariable<C> Comparison;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> Self;

    public override bool IsTrue()
    {
        var hits = Physics2D.RaycastAll(Self.Value.position, (Target.Value.position - Self.Value.position).normalized, 50, LayerMask.GetMask("Players", "Default"))
            .Where(h => !h.collider.isTrigger & h.transform != Self.Value).OrderBy(h => h.distance).ToList();

        var result = hits.FindIndex(h => h.transform == Target.Value) == 0;
        result = Comparison == C.IS ? result : !result;
        return result;
    }

}
