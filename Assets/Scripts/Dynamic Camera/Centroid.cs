using UnityEngine;

public class Centroid : MonoBehaviour
{
    Transform player1;
    Transform player2;

    public Vector3 offset;
    public float smoothSpeed = 1;

    void LateUpdate()
    {
        if (player1 == null && PlayerInputManager.player1 != null)
            player1 = PlayerInputManager.player1.transform;
        if (player2 == null && PlayerInputManager.player2 != null)
            player2 = PlayerInputManager.player2.transform;

        // Wait until both players exist
        if (player1 == null || player2 == null) return;

        Vector3 center = (player1.position + player2.position) / 2f;
        transform.position = center + offset;
    }
}
