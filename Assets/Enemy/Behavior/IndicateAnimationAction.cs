using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Indicate Animation", story: "Indicate [mark] with [Indicator]", category: "Action", id: "802150f09999e0f7793c152deddfdc88")]
public partial class IndicateAnimationAction : Action
{
    public enum C
    {
        QUESTION_MARK,
        EXCLAMATION_MARK,
    }


    [SerializeReference] public BlackboardVariable<C> Mark;
    [SerializeReference] public BlackboardVariable<Animator> Indicator;

    protected override Status OnStart()
    {

        Indicator.Value.SetTrigger("JustIndicated");
        Indicator.Value.SetBool("IsQuestion", Mark.Value == C.QUESTION_MARK);
        return Status.Success;
    }

}

