using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessScreenController : MonoBehaviour
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
