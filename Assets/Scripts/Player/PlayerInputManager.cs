using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;
using UnityEngine.TextCore.Text;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private AnimatorController yellowHamster;
    [SerializeField] private AnimatorOverrideController orangeHamster;
    [SerializeField] private Sprite p2sprite;
    [SerializeField] private GameObject exitInteract;

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

        Animator playerOne = player1.gameObject.GetComponent<Animator>();
        playerOne.runtimeAnimatorController = yellowHamster;

        Animator playerTwo = player1.gameObject.GetComponent<Animator>();
        playerTwo.runtimeAnimatorController = orangeHamster;

        player2.GetComponentInChildren<CarryController>().Einteract.sprite = p2sprite;
    }
}
