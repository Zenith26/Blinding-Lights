using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        PlayerHealth.onPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        PlayerHealth.onPlayerDeath -= EnableGameOverMenu;
    }
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Main_Scene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}