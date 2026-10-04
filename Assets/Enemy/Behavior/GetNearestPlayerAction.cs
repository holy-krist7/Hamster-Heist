using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Nearest Player", story: "[Self] Set Nearest Player in [InView] to [Return]", category: "Action", id: "2806531374ab9308d605c96aa10dcc71")]
public partial class GetNearestPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Return;
    [SerializeReference] public BlackboardVariable<List<GameObject>> InView;
    [SerializeReference] public BlackboardVariable<Transform> Self;


    protected override Status OnStart()
    {
        var nearestPlayer = InView.Value.Where(go => go.CompareTag("Player"))
            .OrderBy(go => Vector2.Distance(Self.Value.position, go.transform.position)).FirstOrDefault();

        if (nearestPlayer == null) return Status.Failure;

        Return.Value = nearestPlayer;

        return Status.Success; 
    }

}

