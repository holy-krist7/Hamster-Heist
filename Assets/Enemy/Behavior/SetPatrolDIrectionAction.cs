using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HH/Set Patrol DIrection", story: "Set [Index] and [isPatrollingBackwards] for [array]", category: "Action", id: "6b03de0ba7bf44856b52d4ccce0b7a81")]
public partial class SetPatrolDIrectionAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Array;
    [SerializeReference] public BlackboardVariable<int> Index;
    [SerializeReference] public BlackboardVariable<bool> IsPatrollingBackwards;

    protected override Status OnStart()
    {
        bool reachedEnd = Index.Value == Array.Value.Count - 1 && !IsPatrollingBackwards.Value;
        bool reachedBeginning = Index.Value == 0 && IsPatrollingBackwards.Value;

        // if reached last point in patrol points
        if (reachedEnd || reachedBeginning)
        {
            // flip direction
            IsPatrollingBackwards.Value = !IsPatrollingBackwards.Value;

        }

        // update index
        Index.Value += IsPatrollingBackwards.Value ? -1 : 1;

        return Status.Success;
    }
}

