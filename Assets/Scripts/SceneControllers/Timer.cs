using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI finalTimerText;
    [SerializeField] GameObject alarm;
    [SerializeField] private float remainingTime;

    private float timer = 0;
    private float finalTimer = 11;

    private void Update()
    {
        if (remainingTime > 1)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            finalTimer -= Time.deltaTime * 0.6f; //Make the 'seconds' of the final timer be slightly longer
            timer += Time.deltaTime;
            timerText.gameObject.SetActive(false);
            finalTimerText.gameObject.SetActive(true);

            if (timer >= 0.75f)
            {
                timer -= 0.75f;
                alarm.SetActive(!alarm.activeSelf);
            }

            if (finalTimer <= 1.5f)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        finalTimerText.text = ((int) finalTimer).ToString();
    }
}
