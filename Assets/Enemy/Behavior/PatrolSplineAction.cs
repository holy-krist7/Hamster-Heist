using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.AppUI.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol Spline", story: "[NavAgent] patrol [Path]", category: "Action", id: "9a5ee08a5d74e3d7f81b7ecb6c9d4d60")]
public partial class PatrolSplineAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> NavAgent;
    [SerializeReference] public BlackboardVariable<SplineContainer> Path;


    const float TARGET_ADVANCE_INTERVAL = 3;
    const float DISTANCE_FROM_DESTINATION_THRESHOLD = 1;

    private float pathLength = -1;

    private float patrolDirection = 1;
    private Vector3 targetPoint;
    private float targetPointLengthRatio = 0;


    protected override Status OnStart()
    {
        if (!Path.Value || !NavAgent.Value)
        {
            return Status.Failure;
        }


        if (pathLength == -1) { pathLength = Path.Value.CalculateLength(); }


        var distanceFromNearestPoint = 
            SplineUtility.GetNearestPoint(Path.Value.Spline, NavAgent.Value.nextPosition - Path.Value.transform.position, out var nearestPoint, out var ratio);


        if (distanceFromNearestPoint > TARGET_ADVANCE_INTERVAL) { targetPointLengthRatio = ratio; }

        // advance targetPoint by TARGET_ADVANCE_INTERVAL
        targetPointLengthRatio = Math.Clamp(targetPointLengthRatio + (patrolDirection * TARGET_ADVANCE_INTERVAL / pathLength), 0, 1);
        targetPoint = Path.Value.EvaluatePosition(targetPointLengthRatio);



        return NavAgent.Value.SetDestination(targetPoint) ? Status.Running : Status.Failure;
    }

    protected override Status OnUpdate()
    {

        // if arrived at destination
        if (NavAgent.Value.remainingDistance <= DISTANCE_FROM_DESTINATION_THRESHOLD && !NavAgent.Value.pathPending)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        NavAgent.Value.ResetPath();

        if (targetPointLengthRatio == 1)
        {
            patrolDirection = -1;
        }
        else if (targetPointLengthRatio == 0)
        {
            patrolDirection = 1;
        }
    }
}

