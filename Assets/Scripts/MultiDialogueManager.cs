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
        displayPanelDemon.SetActive(false);
        displayPanelWitch.SetActive(false);

    }

    void Update()
    {

        //This is an ugly way to iterate to the next valid line; I guess if we have time I can clean it up
        while (currentLine < textLines.Length)
        {

            // Really these should only be set one time when the button is pressed, but I lazy
            string currentTextLine = textLines[currentLine];

            // If this line begins with one of these characters, we don't want to read it (rtf formatting)
            if (currentTextLine.Length == 0 || currentTextLine[0] == '{' || currentTextLine[0] == '}' || currentTextLine[0] == '\\')
            {
                currentLine += 1;
                continue;
            }
            Debug.Log(currentTextLine);

            displayPanelHuman.SetActive(false);
            displayPanelDemon.SetActive(false);
            displayPanelWitch.SetActive(false);



            char characterIndicator = currentTextLine[0];

            switch (characterIndicator)
            {
                case 'P':
                    displayPanelHuman.SetActive(true);
                    displayTextHuman.text = currentTextLine.Substring(2);
                    break;
                case 'M':
                    displayPanelDemon.SetActive(true);
                    displayTextDemon.text = currentTextLine.Substring(2);
                    break;
                case 'W':
                    displayPanelWitch.SetActive(true);
                    displayTextWitch.text = currentTextLine.Substring(2);
                    break;
                default:
                    displayPanelHuman.SetActive(true);
                    displayTextHuman.text = "Yo, check your indicators! START char is " + characterIndicator;
                    break;
            }
            break;
        }
        if (currentLine >=  textLines.Length)
        {
            currentLine = 0;

            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextScene)
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (Input.anyKeyDown)
        {
            currentLine += 1;
        }

    }

}
