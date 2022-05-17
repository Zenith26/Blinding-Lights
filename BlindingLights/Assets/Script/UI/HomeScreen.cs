using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
