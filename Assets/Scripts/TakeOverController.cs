using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeOverController : MonoBehaviour
{
    public GameObject electronPrefab;
    public GameObject neutronPrefab;
    public GameObject protonPrefab;

    public AudioClip selectClip;
    public AudioClip transferSuccessClip;
    public AudioClip transferFailClip;
    public AudioSource audioSource;

    public GameObject neutronAnim;

    private Text counterText;
    private Atom playerAtom;
    private Atom enemyAtom;
    private int maxTime;
    private float timer = 0f;
    private bool lost = false;
    private bool win = false;
    private bool zeroElectStart = false;

    private int remainingElectrons;
    private int remainingProtons;
    private int remainingNeutrons;

    private float restrict = 2f;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GameObject.Find("CountdownText").GetComponent<Text>();
        playerAtom = SavedSettings.playerAtom;
        enemyAtom = SavedSettings.enemyAton;
        //enemyAtom = new Atom(2, 2, 2);
        remainingElectrons = enemyAtom.getElectrons();
        remainingProtons = enemyAtom.getProtons();
        remainingNeutrons = enemyAtom.getNeutrons();

        if (playerAtom.getElectrons() ==0)
        {
            zeroElectStart = true;
        }
        int elecDiff = playerAtom.getElectrons() - enemyAtom.getElectrons();

        maxTime = 20 + elecDiff;
        counterText.text = "" + maxTime;
        timer = 0f;

        Vector3 startPos;
        for (int i = 0; i < remainingElectrons; i++)
        {
            startPos = new Vector3(Random.Range(-restrict, restrict), Random.Range(-restrict, restrict), 0);
            GameObject atom = Instantiate(electronPrefab, startPos, transform.rotation);
        }
        for (int i = 0; i < remainingNeutrons; i++)
        {
            startPos = new Vector3(Random.Range(-restrict, restrict), Random.Range(-restrict, restrict), 0);
            GameObject atom = Instantiate(neutronPrefab, startPos, transform.rotation);
        }
        for (int i = 0; i < remainingProtons; i++)
        {
            startPos = new Vector3(Random.Range(-restrict, restrict), Random.Range(-restrict, restrict), 0);
            GameObject atom = Instantiate(protonPrefab, startPos, transform.rotation);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            if (win == false)
            {
                lost = true;
                //playAudio(transferFailClip);
                if (zeroElectStart)
                {
                    SceneManager.LoadScene("GameOverScene");
                }
                else
                {
                    int elects = playerAtom.getElectrons();
                    elects = elects / 2;

                    playerAtom.setElectrons(elects);
                    SavedSettings.playerAtom = playerAtom;
                    SceneManager.LoadScene("MainScene");
                }


            }

        }
        timer += Time.deltaTime;
        counterText.text = "" + (maxTime - timer); 
    }

    public void claimNeutron()
    {
        
        playAudio(selectClip);
        remainingNeutrons--;
        testVictory();
    }
    public void claimProton()
    {
        playAudio(selectClip);
        remainingProtons--;
        testVictory();
    }
    public void claimElectron()
    {
        playAudio(selectClip);
        remainingElectrons--;
        testVictory();
    }

    private void testVictory()
    {
        if ((remainingNeutrons == 0) && (remainingProtons == 0) && (remainingElectrons == 0))
        {
            win = true;
            //playAudio(transferSuccessClip);
            playerAtom = new Atom(enemyAtom);
            SavedSettings.score += (enemyAtom.maxElectrons * 10);

            SavedSettings.playerAtom = playerAtom;
            SavedSettings.returnFromTransfer = true;
            SavedSettings.successOnTransfer = true;
            
            SceneManager.LoadScene("MainScene");
        }
    }

    private void playAudio(AudioClip clip)
    {
        int volumeSet = PlayerPrefs.GetInt("FXvolumeSet");
        float vol = 1f;
        if (volumeSet > 0)
        {
            int volume = PlayerPrefs.GetInt("FXVolume");
            vol = (float)volume / 100f;
        }

        audioSource.PlayOneShot(clip, vol);

    }
}
