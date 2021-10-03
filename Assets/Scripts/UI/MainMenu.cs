using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MusicController mc;
    // Start is called before the first frame update
    void Start()
    {
        //mc = GameObject.FindObjectOfType<MusicController>();
        //mc.SetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SavedSettings.level = 1;
        SceneManager.LoadScene("MainScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void Optionss()
    {
        SceneManager.LoadScene("OptionsScene");
    }

}
