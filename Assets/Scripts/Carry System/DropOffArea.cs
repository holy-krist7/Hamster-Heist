using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class DropOffArea : MonoBehaviour
{
    [SerializeField] private Scorer scorer;
    private int playersRequired = 2;

    private readonly HashSet<Carryable> itemsInZone = new();
    private readonly List<Carryable> toScore = new();
    private readonly HashSet<CarryController> readyPlayers = new();
    private bool gameEnded;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        if (scorer == null)
        {
            scorer = FindFirstObjectByType<Scorer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var c = other.GetComponentInParent<Carryable>();
        if (c != null)
        {
            itemsInZone.Add(c);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var c = other.GetComponentInParent<Carryable>();
        if (c != null)
        {
            itemsInZone.Remove(c);
        }
    }

    private void Update()
    {
        toScore.Clear();
        itemsInZone.RemoveWhere(c => c == null);

        foreach (var c in itemsInZone)
        {
            if (c.PlayersCarrying.Count == 0)
            {
                toScore.Add(c);
            }
        }

        foreach (var c in toScore)
        {
            scorer.AddScore(c.PointValue);
            itemsInZone.Remove(c);
            Destroy(c.gameObject);
        }
    }

    public void ToggleReady(CarryController player)
    {
        if (gameEnded)
        {
            return;
        }

        if (!readyPlayers.Remove(player))
        {
            readyPlayers.Add(player);
        }

        if (readyPlayers.Count >= playersRequired)
        {
            gameEnded = true;
            Debug.Log("Swithcing scene");
            SceneManager.LoadScene("SuccessScreen");
        }
    }

    public void ClearReady(CarryController player)
    {
        readyPlayers.Remove(player);
    }
}
