using UnityEngine;

public class ClampToCamera : MonoBehaviour
{
    [SerializeField] Camera cam;          // leave empty to use Camera.main
    [SerializeField] Vector2 padding;     // extra margin, e.g. half the player's size

    Rigidbody2D rb;
    Vector2 halfSize;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        // Use the sprite/collider size so the player's edge stops at the screen edge, not its center
        var col = GetComponent<Collider2D>();
        halfSize = col != null ? (Vector2)col.bounds.extents : Vector2.zero;
    }

    void LateUpdate()
    {
        // Camera edges in world space
        Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, -cam.transform.position.z));
        Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, -cam.transform.position.z));

        Vector3 pos = transform.position;
        float clampedX = Mathf.Clamp(pos.x, min.x + halfSize.x + padding.x, max.x - halfSize.x - padding.x);
        float clampedY = Mathf.Clamp(pos.y, min.y + halfSize.y + padding.y, max.y - halfSize.y - padding.y);

        // Stop velocity on any axis that was clamped, so the player doesn't keep pushing into the edge
        if (rb != null)
        {
            Vector2 v = rb.linearVelocity;   // use rb.velocity on Unity versions before 6
            if (!Mathf.Approximately(clampedX, pos.x)) v.x = 0;
            if (!Mathf.Approximately(clampedY, pos.y)) v.y = 0;
            rb.linearVelocity = v;
        }

        transform.position = new Vector3(clampedX, clampedY, pos.z);
    }
}