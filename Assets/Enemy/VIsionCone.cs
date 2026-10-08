using UnityEngine;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour
{

    [SerializeField] private float radius;
    [SerializeField] private float viewAngle;

    public HashSet<Collider2D> CollidersInSight = new();
    [HideInInspector] public int PlayersInSight = 0;

    private PolygonCollider2D collider;
    private int arcResolution = 3;
    private Vector2[] shapePoints;



    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();

        shapePoints = new Vector2[arcResolution + 1];
        shapePoints[0] = Vector2.zero;

        for (int i = 0; i < arcResolution; i++)
        {
            shapePoints[i + 1] = Quaternion.Euler(0, 0, (-viewAngle / 2) + (viewAngle / (arcResolution - 1) * i)) * Vector2.up * radius;
        }

        collider.SetPath(0, shapePoints);
    }


    public Vector2[] GetShapePoints()
    {
        return shapePoints;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidersInSight.Add(collision);
        if (collision.CompareTag("Player")) { PlayersInSight++; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidersInSight.Remove(collision);
        if (collision.CompareTag("Player")) { PlayersInSight--; }
    }

}