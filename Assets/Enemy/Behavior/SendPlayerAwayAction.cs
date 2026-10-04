using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.SceneManagement;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "send player away", story: "[playertarget] go jail", category: "Action", id: "ffc53768d3ed9415bdc6d72aa76ec887")]
public partial class SendPlayerAwayAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Playertarget;

    protected override Status OnStart()
    {
        SceneManager.LoadScene("GameOverScene");
        return Status.Success;
    }

}

