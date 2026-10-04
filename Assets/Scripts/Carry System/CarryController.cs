using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CircleCollider2D))]
public class CarryController : MonoBehaviour
{
    public SpriteRenderer Einteract;
    public Transform Qinteract;
    public Vector2 playerMoveInput;
    private List<Carryable> carryablesInRange = new();
    private Carryable currentlyCarrying;
    private bool isCarryableMounted;
    private bool lockerOnRange = false;
    private bool isHiding = false;

    // references
    private Animator animator;
    private PlayerMovement pm;
    private PositionConstraint pc;
    private DropOffArea dropOffArea;


    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
        pc = GetComponentInParent<PositionConstraint>();
    }

    public void OnCarryableMounted()
    {
        isCarryableMounted = true;
        pm.enabled = false;
    }

    public void OnCarryableUnmounted()
    {
        isCarryableMounted = false;
        pm.enabled = true;
    }


    void BeginCarrying(Carryable c)
    {
        currentlyCarrying = c;
        Einteract.enabled = false;

        if (c.PlayersCarrying.Count == 0)
        {
            pc.AddSource(new() { sourceTransform = c.transform, weight = 1 });
            pc.translationOffset = c.Bounds.extents * new Vector2(-1, -0.8f);

            animator.SetBool("isSecondCarrier", false);
        }
        else
        {
            pc.AddSource(new() { sourceTransform = c.transform, weight = 1 });
            pc.translationOffset = c.Bounds.extents * new Vector2(1, -0.8f);

            animator.SetBool("isSecondCarrier", true);
        }
        pm.enabled = false;

        pc.constraintActive = true;
        animator.SetBool("isCarrying", true);
        animator.SetBool("isHeavy", c.PlayersNeeded > 1);

        c.TryCarry(this);
    }

    void DropCarryable()
    {
        currentlyCarrying.TryDrop(this);
        currentlyCarrying = null;

        pc.constraintActive = false;
        pc.RemoveSource(0);

        pm.enabled = true;
        animator.SetBool("isCarrying", false);
    }

    public void OnCarry(InputAction.CallbackContext context)
    {


        if (context.performed)
        {
            carryablesInRange.RemoveAll(c => c == null);

            if (currentlyCarrying != null)
            {
                DropCarryable();
            }
            else if (carryablesInRange.Count > 0)
            {
                BeginCarrying(carryablesInRange[0]);
            }

            if (lockerOnRange)
            {
                Hide();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMoveInput = context.ReadValue<Vector2>();
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed && dropOffArea != null)
        {
            dropOffArea.ToggleReady(this);
        }
    }

    void Hide()
    {
        isHiding = !isHiding;
        var sr = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        var rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        var col = transform.parent.gameObject.GetComponent<CircleCollider2D>();

        if (isHiding)
        {
            Debug.Log("Player Is Hiding");
            sr.enabled = false;
            rb.linearVelocity = Vector2.zero;
            col.enabled = false;
            pm.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Debug.Log("Player Exited Locker");
            sr.enabled = true;
            col.enabled = true;
            pm.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var zone = collision.GetComponent<DropOffArea>();
        if (zone != null)
        {
            dropOffArea = zone;
            Qinteract.gameObject.SetActive(true);
        }

        var carryable = collision.GetComponent<Carryable>();
        if (carryable != null)
        {
            carryablesInRange.Add(carryable);
            carryablesInRange = carryablesInRange.OrderBy(c => Vector2.Distance(transform.position, c.transform.position)).ToList();
            Einteract.enabled = true;

        }

        var locker = collision.GetComponent<Locker>();
        if (locker != null)
        {
            Debug.Log("Locker In Range");
            lockerOnRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var zone = collision?.GetComponent<DropOffArea>();
        if (zone != null && zone == dropOffArea)
        {
            zone.ClearReady(this);
            dropOffArea = null;
            Qinteract.gameObject.SetActive(false);
        }
        var carryable = collision.GetComponent<Carryable>();
        if (carryable != null)
        {
            carryablesInRange.Remove(carryable);
            Einteract.enabled = false;
        }
        var locker = collision.GetComponent<Locker>();
        if (locker != null)
        {
            lockerOnRange = false;

            if (carryablesInRange.Count == 0)
            {
                Einteract.enabled = false;

            }
        }

    }
}
