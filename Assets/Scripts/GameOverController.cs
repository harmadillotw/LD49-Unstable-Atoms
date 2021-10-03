using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Text scoreText;
    private int highScore;
    private string displayText;
    // Start is called before the first frame update
    void Start()
    {
        displayText = "Your Score: " + SavedSettings.score;
       if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
       if ( SavedSettings.score  > highScore)
        {
            displayText += "\n\n\n";
            displayText += "Congratulations.\nYou got a new high score.";
            PlayerPrefs.SetInt("HighScore", SavedSettings.score);
        }
        else
        {
            displayText += "\n\n\n";
            displayText += "Current high score is " + highScore;
        }
        scoreText.text = displayText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
