using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public BoxCollider2D tutorialActivation;
    public GameObject player;

    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (tutorialActivation.IsTouching(playerCollider))
        {

            panel.SetActive(true);
        } else
        {

            panel.SetActive(false);

        }
    }
}
