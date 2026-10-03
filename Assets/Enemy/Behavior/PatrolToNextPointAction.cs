using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HH/Patrol To Next Point", story: "Patrol [Self] to [index] on [PatrolPoints]", category: "Action", id: "57caf4a1f5eaf4535aed87c6daca9ede")]
public partial class PatrolToNextPointAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Index;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoints;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private NavMeshAgent navAgent;

    protected override Status OnStart()
    {
        if (Index.Value >= PatrolPoints.Value.Count || Index.Value < 0)
        {
            return Status.Failure;
        }

        navAgent = Self.Value.GetComponent<NavMeshAgent>();
        navAgent.SetDestination(PatrolPoints.Value[Index.Value].transform.position);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        // if NavAgent has reached the destination
        if (navAgent.remainingDistance <= navAgent.stoppingDistance && !navAgent.pathPending)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

