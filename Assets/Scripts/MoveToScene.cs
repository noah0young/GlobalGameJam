using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{

    public int sceneDestination;
    // Start is called before the first frame update
    public void changeScene()
    {
        SceneManager.LoadScene(sceneDestination);
    }
}
