using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToExit()
    {
        Application.Quit();
    }
    public void GoToConfig()
    {
        SceneManager.LoadScene("Config");
    }
}
