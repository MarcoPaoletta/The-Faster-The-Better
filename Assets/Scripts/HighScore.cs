using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;

    public static int highScore;

    void Start()
    {
        SetHighScoreText();
        CheckVisibility();
    }

    void CheckVisibility()
    {
        if(highScore == 0)
        {
            highScoreText.text = "? ms";
        }
    }

    void SetHighScoreText()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = highScore.ToString() + " ms";
    }
}
