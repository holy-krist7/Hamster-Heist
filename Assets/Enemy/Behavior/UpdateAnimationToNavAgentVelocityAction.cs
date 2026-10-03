using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Update Animation to NavAgent Velocity", story: "[Self] update walking animation", category: "Action", id: "8cfc43c740082c4c247fd5e1bedb74ea")]
public partial class UpdateAnimationToNavAgentVelocityAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private NavMeshAgent navAgent;
    private Animator animator;


    protected override Status OnStart()
    {
        if (navAgent == null)
        {
            navAgent = Self.Value.GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = Self.Value.GetComponentInChildren<Animator>();
        }


        animator.SetFloat("x", navAgent.velocity.normalized.x);
        animator.SetFloat("y", navAgent.velocity.normalized.y);


        return Status.Success;
    }
}

