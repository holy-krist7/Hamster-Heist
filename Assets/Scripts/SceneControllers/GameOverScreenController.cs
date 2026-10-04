using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    public void OnRestartClick()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
