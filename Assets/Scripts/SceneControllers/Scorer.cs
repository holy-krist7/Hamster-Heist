using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int FinalScore {  get; private set; }

    private int score = 0;

    public const int OneStar = 4000;
    public const int TwoStars = 8000;
    public const int ThreeStars = 12000;

    public void AddScore(int amount)
    {
        score += amount;
        FinalScore = score;
        scoreText.text = score.ToString();
    }

    public static int GetStars(int points)
    {
        if (points >= ThreeStars) return 3;
        if (points >= TwoStars) return 2;
        if (points >= OneStar) return 1;
        return 0;
    }

    private void Start()
    {
        score = 0;
        FinalScore = 0;
        scoreText.text = "0";
    }
}
