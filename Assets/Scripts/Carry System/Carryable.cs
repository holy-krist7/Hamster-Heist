using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Carryable : MonoBehaviour
{
    public int PlayersNeeded;
    public int pointValue;

    [HideInInspector] public Bounds Bounds;
    
    [HideInInspector] public List<CarryController> PlayersCarrying = new();

    private Rigidbody2D rb;

    private void Awake()
    {
        Bounds = GetComponent<BoxCollider2D>().bounds;
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }


    private void Update()
    {
        if (PlayersCarrying.Count >= PlayersNeeded)
        {
            Vector2 avgVel = new();
            foreach ( var player in PlayersCarrying)
            {
                avgVel += player.playerMoveInput.normalized;
            }
            avgVel /= PlayersCarrying.Count;

            rb.linearVelocity = avgVel * 5;
        }


        if (rb.bodyType == RigidbodyType2D.Dynamic) return;
        var a = new List<Collider2D>();
        if (rb.Overlap(new ContactFilter2D { useLayerMask = true, layerMask = LayerMask.GetMask("Default")},a) == 0) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void TryCarry(CarryController carryController)
    {
        if (!PlayersCarrying.Contains(carryController))
        {
            PlayersCarrying.Add(carryController);
        }

        TryMount();
    }

    public void TryDrop(CarryController carryController)
    {
        if (PlayersCarrying.Contains(carryController))
        {
            PlayersCarrying.Remove(carryController);
        }

        TryUnmount();
    }


    void TryMount()
    {
        if (PlayersCarrying.Count >= PlayersNeeded)
        {
            foreach (var player in PlayersCarrying)
            {
                player.OnCarryableMounted();
            }
        }

    }

    void TryUnmount()
    {
        foreach (var player in PlayersCarrying)
        {
            player.OnCarryableUnmounted();
        }

        if (PlayersCarrying.Count == 0)
        {
            rb.linearVelocity = Vector2.zero;
        }

    }


    private void OnDestroy()
    {
        TryUnmount();
    }

}
