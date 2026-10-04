using UnityEngine;

public class VisionCone : MonoBehaviour
{

    [SerializeField] private EnemyVisionEvent eventChannel;
    [SerializeField] private float radius;
    [SerializeField] private float viewAngle;

    private PolygonCollider2D collider;
    private int arcResolution = 3;
    private Vector2[] points;
    


    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();

        points = new Vector2[arcResolution + 1];
        points[0] = Vector2.zero;

        for (int i = 0; i < arcResolution; i++)
        {
            points[i + 1] = Quaternion.Euler(0, 0, (-viewAngle / 2) + (viewAngle / (arcResolution - 1) * i)) * Vector2.up * radius;
        }

        collider.SetPath(0, points);
    }


    public Vector2[] GetPoints()
    {
        return points;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        eventChannel.SendEventMessage(transform.parent.gameObject, collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        eventChannel.SendEventMessage(transform.parent.gameObject, collision.gameObject, false);
    }

}