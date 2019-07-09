using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text perfectText;
    public RowGenerator rowGenerator;

    public void Score(int number)
    {
        score +=number*5 ;
        scoreText.text = "Score: " + score;
    }

    public void Resume()
    {
        UnfreezeGame();
    }

    public void Pause()
    {
        FreezeGame();   
    }

    public void Restart()
    {
        SceneManager.LoadScene("Bubble Pops");
        Time.timeScale = 1.0f;
    }

    public void FreezeGame()
    {
        Time.timeScale = 0.0f;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1.0f;
    }
}
