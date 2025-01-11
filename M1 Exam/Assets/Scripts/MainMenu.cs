using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
