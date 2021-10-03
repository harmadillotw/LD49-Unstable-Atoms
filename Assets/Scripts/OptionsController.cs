using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public InputField fxVolInputField;
    public Text fxVolInputFieldText;
    public InputField mVolInputField;
    public Text mVolInputFieldText;
    public Toggle mMuteToggle;

    public int fxVolume;
    public int mVolume;

    private MusicController mc;
    // Start is called before the first frame update
    void Start()
    {
        fxVolInputField = GameObject.Find("SVolInputField").GetComponent<InputField>();
        fxVolInputFieldText = fxVolInputField.GetComponentInChildren<Text>();
        mVolInputField = GameObject.Find("MVolInputField").GetComponent<InputField>();
        mVolInputFieldText = mVolInputField.GetComponentInChildren<Text>();
        mMuteToggle = GameObject.Find("Toggle").GetComponent<Toggle>();

        mc = GameObject.FindObjectOfType<MusicController>();

        int FXVolumeSet = PlayerPrefs.GetInt("FXvolumeSet");
        fxVolume = 100;
        if (FXVolumeSet > 0)
        {
            fxVolume = PlayerPrefs.GetInt("FXVolume");
        }
        fxVolInputFieldText.text = "" + fxVolume;

        int MVolumeSet = PlayerPrefs.GetInt("MVolumeSet");
        mVolume = 100;
        if (MVolumeSet > 0)
        {
            mVolume = PlayerPrefs.GetInt("MVolume");
        }
        mVolInputFieldText.text = "" + mVolume;

        int mMute = PlayerPrefs.GetInt("MVolumeMute", 0);
        if (mMute == 1)
        {
            mMuteToggle.isOn = true;
        }
        else
        {
            mMuteToggle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateFXVolume()
    {
        string volumeString = fxVolInputField.text;
        int volumeInt = 0;
        if (int.TryParse(volumeString, out volumeInt))
        {
            PlayerPrefs.SetInt("FXvolumeSet", 1);
            PlayerPrefs.SetInt("FXVolume", volumeInt);
        }
        
    }

    public void updateMVolume()
    {
        string volumeString = mVolInputField.text;
        int volumeInt = 0;
        if (int.TryParse(volumeString, out volumeInt))
        {
            PlayerPrefs.SetInt("MVolumeSet", 1);
            PlayerPrefs.SetInt("MVolume", volumeInt);
        }
        
        mc.SetVolume();

    }

    public void updateMMuteVolume()
    {
        if (mMuteToggle.isOn)
        {
            PlayerPrefs.SetInt("MVolumeMute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MVolumeMute", 0);
        }
        mc.MuteVolume();
    }
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }


}