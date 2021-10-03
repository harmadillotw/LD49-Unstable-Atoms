using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public int level = 1;
    public Image imageLevel1;
    public Image imageLevel2;
    public Image imageLevel3;
    public Image imageLevel4;
    public Image imageLevel5;
    public Image imageLevelExit;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        level = SavedSettings.level;
        //imageLevel1 = GameObject.Find("LevelImage1").GetComponent<Image>();
        //imageLevel2 = GameObject.Find("LevelImage2").GetComponent<Image>();
        //imageLevel3 = GameObject.Find("LevelImage3").GetComponent<Image>();
        //imageLevel4 = GameObject.Find("LevelImage4").GetComponent<Image>();
        //imageLevel5 = GameObject.Find("LevelImage5").GetComponent<Image>();
        //imageLevelExit = GameObject.Find("LevelImageExit").GetComponent<Image>();
        updateImage();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
        {
            if (level != 6)
            {
                level++;
                updateImage();
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.A)))
        {
            if (level != 1)
            {
                level--;
                updateImage();
            }
        }
        if ((Input.GetKeyDown(KeyCode.Return)) || (Input.GetKeyDown(KeyCode.Space)))
        {
            ExecuteEvent();
        }
    }

    private void updateImage()
    {
        switch (level)
        {
            case 1:
                imageLevel1.gameObject.SetActive(true);
                imageLevel2.gameObject.SetActive(false);
                imageLevel3.gameObject.SetActive(false);
                imageLevel4.gameObject.SetActive(false);
                imageLevel5.gameObject.SetActive(false);
                imageLevelExit.gameObject.SetActive(false);

                break;
            case 2:
                imageLevel1.gameObject.SetActive(false);
                imageLevel2.gameObject.SetActive(true);
                imageLevel3.gameObject.SetActive(false);
                imageLevel4.gameObject.SetActive(false);
                imageLevel5.gameObject.SetActive(false);
                imageLevelExit.gameObject.SetActive(false);
                break;
            case 3:
                imageLevel1.gameObject.SetActive(false);
                imageLevel2.gameObject.SetActive(false);
                imageLevel3.gameObject.SetActive(true);
                imageLevel4.gameObject.SetActive(false);
                imageLevel5.gameObject.SetActive(false);
                imageLevelExit.gameObject.SetActive(false);
                break;
            case 4:
                imageLevel1.gameObject.SetActive(false);
                imageLevel2.gameObject.SetActive(false);
                imageLevel3.gameObject.SetActive(false);
                imageLevel4.gameObject.SetActive(true);
                imageLevel5.gameObject.SetActive(false);
                imageLevelExit.gameObject.SetActive(false);
                break;
            case 5:
                imageLevel1.gameObject.SetActive(false);
                imageLevel2.gameObject.SetActive(false);
                imageLevel3.gameObject.SetActive(false);
                imageLevel4.gameObject.SetActive(false);
                imageLevel5.gameObject.SetActive(true);
                imageLevelExit.gameObject.SetActive(false);
                break;
            case 6:
                imageLevel1.gameObject.SetActive(false);
                imageLevel2.gameObject.SetActive(false);
                imageLevel3.gameObject.SetActive(false);
                imageLevel4.gameObject.SetActive(false);
                imageLevel5.gameObject.SetActive(false);
                imageLevelExit.gameObject.SetActive(true);
                break;
            
        }
        if (level == 6)
        {
            levelText.text = "End Game";
        }
        else
        {
            levelText.text = "Level: " + level;
        }
    }

    private void ExecuteEvent()
    {
        SavedSettings.level = level;
        if (level == 6)
        {
            //EXIT
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            //goto level
            SceneManager.LoadScene("MainScene");
        }
    }
}
