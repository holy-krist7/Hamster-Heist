using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Player in Vision", story: "[Return] Nearest Player in [Vision]", category: "Action", id: "53a458a0d02a10a78133891386004d61")]
public partial class GetPlayerInVisionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Return;
    [SerializeReference] public BlackboardVariable<VisionCone> Vision;

    protected override Status OnStart()
    {
        if (Return == null || Vision == null)
        {
            return Status.Failure;
        }

        Return.Value = Vision.Value.CollidersInSight.Where(c => c.CompareTag("Player")).OrderBy(c => Vector2.Distance(c.transform.position, GameObject.transform.position)).First().gameObject;
        return Status.Success;
    }

}

