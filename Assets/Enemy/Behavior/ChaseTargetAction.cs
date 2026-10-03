using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HH/Chase Target", story: "[Self] Chase [Target]", category: "Action", id: "9bf0fa5c1b904da94ccf79b142097ea1")]
public partial class ChaseTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private NavMeshAgent NavAgent;


    protected override Status OnStart()
    {
        if (NavAgent == null)
        {
            NavAgent = Self.Value.GetComponentInChildren<NavMeshAgent>();
        }

        NavAgent.SetDestination(Target.Value.transform.position);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if ((Vector2) NavAgent.destination != (Vector2) Target.Value.transform.position)
        {
            NavAgent.SetDestination(Target.Value.transform.position);
        }

        if (NavAgent.transform.position != Target.Value.transform.position)
        {
            return Status.Running;
        }


        return Status.Success;
    }

    protected override void OnEnd()
    {
        NavAgent.ResetPath();
    }
}

