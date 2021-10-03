using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{

    private AudioSource audioSource;

    

    private void Awake()
    {
        {
            DontDestroyOnLoad(transform.gameObject);
            audioSource = GetComponentInChildren<AudioSource>();
            SetVolume();
            MuteVolume();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume()
    {

        int volumeSet = PlayerPrefs.GetInt("MVolumeSet");
        float vol = 1f;
        if (volumeSet > 0)
        {
            int volume = PlayerPrefs.GetInt("MVolume");
            vol = (float)volume / 100f;
        }
        audioSource.volume = vol;
        PlayMusic();

    }

    public void MuteVolume( )
    {
        int mMute = PlayerPrefs.GetInt("MVolumeMute", 0);
        if (mMute == 1)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
        PlayMusic();
    }

}
