using TMPro;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Update()
    {
        scoreText.text = score.ToString();
    }
}
