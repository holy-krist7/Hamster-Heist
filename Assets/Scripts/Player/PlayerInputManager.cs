using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public static PlayerInput player1;
    public static PlayerInput player2;
    
    private void Start()
    {
        player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "WASD", pairWithDevice: Keyboard.current);
        player1.transform.position = spawnPoints[0].position;

        player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Arrows", pairWithDevice: Keyboard.current);
        player2.transform.position = spawnPoints[1].position;
    }
}
