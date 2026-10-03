using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Animation Speed", story: "Set [Self] Animation Speed to [Speed]", category: "Action", id: "346d37a1f948c8d311ac75aea59bce52")]
public partial class SetAnimationSpeedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Animator animator;

    protected override Status OnStart()
    {
        if (animator == null)
        {
            animator = Self.Value.GetComponentInChildren<Animator>();
        }

        animator.speed = Speed.Value;
        return Status.Success;
    }

}

