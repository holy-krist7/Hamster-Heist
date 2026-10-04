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
    
    private void Start()
    {
        player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "WASD", pairWithDevice: Keyboard.current);
        player1.transform.position = spawnPoints[0].position;
        Animator playerOne = player1.gameObject.GetComponent<Animator>();
        playerOne.runtimeAnimatorController = yellowHamster;


        player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Arrows", pairWithDevice: Keyboard.current);
        player1.transform.position = spawnPoints[1].position;
        Animator playerTwo = player1.gameObject.GetComponent<Animator>();
        playerTwo.runtimeAnimatorController = orangeHamster;
    }
}
