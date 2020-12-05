using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;

    private void Start()
    {
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(scene.buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
