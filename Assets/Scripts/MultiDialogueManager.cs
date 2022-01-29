using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MultiDialogueManager : MonoBehaviour
{

    public GameObject displayPanelHuman;
    public TextMeshProUGUI displayTextHuman;

    public GameObject displayPanelDemon;
    public TextMeshProUGUI displayTextDemon;

    public GameObject displayPanelWitch;
    public TextMeshProUGUI displayTextWitch;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (textFile != null)
        {
            textLines = textFile.text.Split('\n');
        }

        displayPanelHuman.SetActive(false);
        displayTextHuman.color = Color.white;
        displayPanelDemon.SetActive(false);
        displayTextDemon.color = Color.red;
        displayPanelWitch.SetActive(false);
        displayTextWitch.color = Color.magenta;

    }

    void Update()
    {
        if (currentLine < textLines.Length)
        {

            // Really these should only be set one time when the button is pressed, but I lazy
            string currentTextLine = textLines[currentLine];

            displayPanelHuman.SetActive(false);
            displayPanelDemon.SetActive(false);
            displayPanelWitch.SetActive(false);



            char characterIndicator = currentTextLine[0];

            switch (characterIndicator)
            {
                case 'h':
                    displayPanelHuman.SetActive(true);
                    displayTextHuman.text = currentTextLine.Substring(2);
                    break;
                case 'd':
                    displayPanelDemon.SetActive(true);
                    displayTextDemon.text = currentTextLine.Substring(2);
                    break;
                case 'w':
                    displayPanelWitch.SetActive(true);
                    displayTextWitch.text = currentTextLine.Substring(2);
                    break;
                default:
                    displayPanelHuman.SetActive(true);
                    displayTextHuman.text = "Yo, check your indicators!";
                    break;
            }

        }
        else
        {
            currentLine = 0;

            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(1);
        }

        if (Input.anyKeyDown)
        {
            currentLine += 1;
        }

    }

}
