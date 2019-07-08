using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadPlayerScene()
    {
        SceneManager.LoadScene("Bubble Pops");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
