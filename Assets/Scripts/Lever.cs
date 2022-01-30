using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject[] pairedObjects;
    private bool inRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Test");
            Activate();
        }
    }

    private void Activate()
    {
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
