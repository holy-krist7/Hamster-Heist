using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is a Player in View", story: "[Self] Is Player in [InView] adn return to [SeenPlayer]", category: "Conditions", id: "0dd990d8b8232bf46b38df3491aa3984")]
public partial class IsAPlayerInViewCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> InView;
    [SerializeReference] public BlackboardVariable<GameObject> SeenPlayer;

    public override bool IsTrue()
    {
        if (InView.Value == null) return false;

        var player = InView.Value.Where(go => go.CompareTag("Player"))
            .OrderBy(go => Vector2.Distance(Self.Value.transform.position, go.transform.position)).FirstOrDefault();
        
        if (SeenPlayer != null)
        {
            SeenPlayer.Value = player;
        }

        return player != null;
    }

}
