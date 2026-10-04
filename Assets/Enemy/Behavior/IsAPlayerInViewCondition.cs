using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is a Player in View", story: "Is Player [Condition] [InView]", category: "Conditions", id: "0dd990d8b8232bf46b38df3491aa3984")]
public partial class IsAPlayerInViewCondition : Condition
{
    public enum C
    {
        IS,
        IS_NOT,
    }

    [SerializeReference] public BlackboardVariable<List<GameObject>> InView;
    [SerializeReference] public BlackboardVariable<C> Condition;

    public override bool IsTrue()
    {
        var res = InView.Value.Any(go => go.CompareTag("Player"));
        if (Condition.Value == C.IS_NOT) res = !res;

        return res;
    }

}
