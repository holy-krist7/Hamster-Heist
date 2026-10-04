using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private AnimatorController yellowHamster;
    [SerializeField] private AnimatorOverrideController orangeHamster;

    public static PlayerInput player1;
    public static PlayerInput player2;
    
    private void Awake()
    {
        var pads = Gamepad.all;

        if (pads.Count >= 2)
        {
            player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: pads[0]);
            player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: pads[1]);
        }
        else
        {
            // Fallback so you can still test without two controllers
            player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "WASD", pairWithDevice: Keyboard.current);
            player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Arrows", pairWithDevice: Keyboard.current);
        }

        player1.transform.position = spawnPoints[0].position;
        player2.transform.position = spawnPoints[1].position;
    }
}
