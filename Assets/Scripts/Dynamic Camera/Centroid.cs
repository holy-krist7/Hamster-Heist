using UnityEngine;

public class Centroid : MonoBehaviour
{
    Transform player1;
    Transform player2;

    public Vector3 offset;
    public float smoothSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player1 = PlayerInputManager.player1.transform;
        player2 = PlayerInputManager.player2.transform;
        transform.position = player1.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        totalX += player1.position.x + player2.position.x;
        totalY += player1.position.y + player2.position.y;
        totalZ += player1.position.z + player2.position.z;

        float centerX = totalX / 2;
        float centerY = totalY / 2;
        float centerZ = totalZ / 2;

        transform.position = new Vector3(centerX, centerY, centerZ);
    }
}
