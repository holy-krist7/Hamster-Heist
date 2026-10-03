using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HH/Try to update View Array", story: "[hasEntered] Try to add/remove [collider] to [InViewArray] [arraySize]", category: "Action", id: "f0ac8d97025ed5d398072a6212ef2499")]
public partial class TryUpdateViewArray : Action
{
    [SerializeReference] public BlackboardVariable<bool> HasEntered;
    [SerializeReference] public BlackboardVariable<GameObject> Collider;
    [SerializeReference] public BlackboardVariable<List<GameObject>> InViewArray;
    [SerializeReference] public BlackboardVariable<int> ArraySize;

    protected override Status OnStart()
    {
        if (Collider.Value.CompareTag("Player"))
        {
            if (HasEntered.Value)
            {
                InViewArray.Value.Add(Collider.Value);
                ArraySize.Value++;
            }
            else
            {
                InViewArray.Value.Remove(Collider.Value);
                ArraySize.Value--;
            }

            return Status.Success;
        }

        return Status.Failure;
    }
}

