using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button button;
    public GameObject highScore;
    public AudioSource music;
    public AudioSource click;
    public AudioSource lose;
    public AudioSource win;
    public Text mainTitle;
    public Text subtitle;
    public Text tapAnywhere;
    public Image imageButton;
    public Image touchIcon;
    public Image clockIcon;

    float time;
    int ms;
    int counter;

    public void OnButtonClicked()
    {
        if(imageButton.color != Color.blue && imageButton.color != Color.magenta)
        {
            HideThings();
            ChangeColorToRed();
            CreateTimeAndChangeColorToGreen();
        }

        StopMusic();
        PlayClickSound();
    }

    void HideThings()
    {
        mainTitle.text = "";
        subtitle.text = "";
        tapAnywhere.text = "";
        touchIcon.gameObject.SetActive(false);
        highScore.gameObject.SetActive(false);
    }

    void ChangeColorToRed()
    {
        imageButton.color = Color.red;

        if(mainTitle.text != "Too early, restarting")
        {
            mainTitle.text = "Wait for green";
        }
    }

    void CreateTimeAndChangeColorToGreen()
    {
        float randomSeconds = Random.Range(3f, 6f);
        Invoke("ChangeColorToGreen", randomSeconds);
    }

    void ChangeColorToGreen()
    {
        if(imageButton.color != Color.magenta)
        {
            imageButton.color = Color.green;
            mainTitle.text = "Click!";
            button.interactable = true;
        }
    }

    void StopMusic()
    {
        music.gameObject.SetActive(false);
    }

    void PlayClickSound()
    {
        if(imageButton.color != Color.blue && imageButton.color != Color.magenta)
        {
            click.Play();
        }
    }

    void Update()
    {
        if(button.GetComponent<PointerManager>().pointerDown)
        {
            CheckColor();
        }
        if(counter >= 2 && !button.GetComponent<PointerManager>().pointerDown)
        {
            RestartScene();
        }
        if(imageButton.color == Color.green)
        {
            Invoke("SetTime", 0.001f);
        }
        if(imageButton.color == Color.red && button.GetComponent<PointerManager>().pointerDown)
        {
            Lose();
        }
    }

    void CheckColor()
    {
        if(imageButton.color == Color.green)
        {
            button.GetComponent<PointerManager>().pointerDown = false;
            win.Play();
            imageButton.color = Color.blue;
            mainTitle.text = ms.ToString() + " ms";
            clockIcon.gameObject.SetActive(true);
            tapAnywhere.text = "Tap anywhere to restart";
            SetHighScore();
        }
        else if(imageButton.color == Color.blue)
        {
            counter += 1;
        }
        else if(imageButton.color == Color.red)
        {
            Lose();
        }
    }

    void SetHighScore()
    {
        if(ms < HighScore.highScore || HighScore.highScore == 0)
        {
            HighScore.highScore = ms;
            PlayerPrefs.SetInt("HighScore", HighScore.highScore);
        }
    }

    void Lose()
    {
        imageButton.color = Color.magenta;
        lose.Play();
        button.interactable = false;
        mainTitle.text = "Too early, restarting";
        Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetTime()
    {
        ms += 1;
    }
}