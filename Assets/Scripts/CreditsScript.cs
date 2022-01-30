using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (PlayerPrefs.HasKey("justWon") && PlayerPrefs.GetInt("justWon") == 1)
            {
                // This should be the after credits scene
                PlayerPrefs.SetInt("justWon", 0);
                SceneManager.LoadScene(8);
            } 
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
