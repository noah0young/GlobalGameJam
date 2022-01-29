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
            displayText.text = textLines[currentLine];
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
