using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    public GameObject[] pairedObjects;
    private bool inRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Test");
            Activate();
        }
    }

    private void Activate()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("PullLever");
        for (int i = 0; i < pairedObjects.Length; i++)
        {
            GameObject pairedObject = pairedObjects[i];
            
            if (pairedObject.CompareTag("Pillar"))
            {
                pairedObject.SetActive(false);
            }
            else if (pairedObject.CompareTag("Door"))
            {
                pairedObject.GetComponent<Door>().open = true;
            }
            //other stuff for doors, presumably
        }
    }
}
