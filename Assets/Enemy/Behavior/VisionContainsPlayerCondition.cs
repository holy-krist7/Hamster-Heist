using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Vision Contains Player", story: "[Vision] contains a Player", category: "Conditions", id: "980a876b273225a298d5336fba1f310c")]
public partial class VisionContainsPlayerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<VisionCone> Vision;

    public override bool IsTrue()
    {
        return Vision.Value.PlayersInSight > 0;
    }
}
