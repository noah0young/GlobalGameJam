using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{

    public int sceneDestination;
    //public string sceneDestinationStr;
    // Start is called before the first frame update
    public void changeScene()
    {
        SceneManager.LoadScene(sceneDestination);
    }

    public void changeSceneString(string sceneDestinationStr)
    {
        SceneManager.LoadScene(sceneDestinationStr);
    }
}
