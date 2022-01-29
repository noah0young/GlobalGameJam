using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueImporter : MonoBehaviour
{
    public TextMeshProUGUI displayText;

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
    }

    void Update()
    {
        if (currentLine < textLines.Length)
        {

            // Really these should only be set one time when the button is pressed, but I lazy
            string currentTextLine = textLines[currentLine];

            displayText.text = currentTextLine.Substring(2);


            char characterIndicator = currentTextLine[0];

            switch(characterIndicator){
                case 'h':
                    displayText.color = Color.white;
                    break;
                case 'd':
                    displayText.color = Color.red;
                    break;
                case 'w':
                    displayText.color = Color.magenta;
                    break;
                default:
                    displayText.text = "Yo, check your indicators!";
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
