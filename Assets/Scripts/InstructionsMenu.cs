using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour
{

    public int level = 1;
    public Button nextButton;
    public Button previousButton;
    public Image instructionsImage1;
    public Image instructionsImage2;
    public Image instructionsImage3;
    public Image instructionsImage4;
    //public Image instructionsImage5;

    public Text instructionsText1;
    public Text instructionsText2;
    public Text instructionsText3;
    public Text instructionsText4;
    public Text instructionsText5;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        previousButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        instructionsImage1.gameObject.SetActive(true);
        instructionsImage2.gameObject.SetActive(false);
        instructionsImage3.gameObject.SetActive(false);
        instructionsImage4.gameObject.SetActive(false);
        instructionsText1.gameObject.SetActive(false);
        instructionsText2.gameObject.SetActive(false);
        instructionsText3.gameObject.SetActive(false);
        instructionsText4.gameObject.SetActive(false);
        instructionsText5.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        level++;
        updateDisplay();
    }
    public void Previous()
    {
        level--;
        updateDisplay();
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void updateDisplay()
    {
        switch (level)
        {
            case 1:
                previousButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
                instructionsImage1.gameObject.SetActive(true);
                instructionsImage2.gameObject.SetActive(false);
                instructionsText1.gameObject.SetActive(false);
                instructionsText2.gameObject.SetActive(false);
                instructionsText3.gameObject.SetActive(false);
                instructionsText4.gameObject.SetActive(false);
                instructionsText5.gameObject.SetActive(false);
                break;
            case 2:
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                instructionsImage1.gameObject.SetActive(false);
                instructionsImage2.gameObject.SetActive(true);
                instructionsImage3.gameObject.SetActive(false);
                instructionsImage4.gameObject.SetActive(false);
                instructionsText1.gameObject.SetActive(true);
                instructionsText2.gameObject.SetActive(false);
                instructionsText3.gameObject.SetActive(false);
                instructionsText4.gameObject.SetActive(false);
                instructionsText5.gameObject.SetActive(false);
                break;
            case 3:
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                instructionsImage1.gameObject.SetActive(false);
                instructionsImage2.gameObject.SetActive(true);
                instructionsImage3.gameObject.SetActive(false);
                instructionsImage4.gameObject.SetActive(false);
                instructionsText1.gameObject.SetActive(false);
                instructionsText2.gameObject.SetActive(true);
                instructionsText3.gameObject.SetActive(false);
                instructionsText4.gameObject.SetActive(false);
                instructionsText5.gameObject.SetActive(false);
                break;
            case 4:
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                instructionsImage1.gameObject.SetActive(false);
                instructionsImage2.gameObject.SetActive(true);
                instructionsImage3.gameObject.SetActive(false);
                instructionsImage4.gameObject.SetActive(false);
                instructionsText1.gameObject.SetActive(false);
                instructionsText2.gameObject.SetActive(false);
                instructionsText3.gameObject.SetActive(true);
                instructionsText4.gameObject.SetActive(false);
                instructionsText5.gameObject.SetActive(false);
                break;
            case 5:
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                instructionsImage1.gameObject.SetActive(false);
                instructionsImage2.gameObject.SetActive(false);
                instructionsImage3.gameObject.SetActive(true);
                instructionsImage4.gameObject.SetActive(false);
                instructionsText1.gameObject.SetActive(false);
                instructionsText2.gameObject.SetActive(false);
                instructionsText3.gameObject.SetActive(false);
                instructionsText4.gameObject.SetActive(true);
                instructionsText5.gameObject.SetActive(false);
                break;
            case 6:
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                instructionsImage1.gameObject.SetActive(false);
                instructionsImage2.gameObject.SetActive(false);
                instructionsImage3.gameObject.SetActive(false);
                instructionsImage4.gameObject.SetActive(true);
                instructionsText1.gameObject.SetActive(false);
                instructionsText2.gameObject.SetActive(false);
                instructionsText3.gameObject.SetActive(false);
                instructionsText4.gameObject.SetActive(false);
                instructionsText5.gameObject.SetActive(true);
                break;
        }
    }

}
