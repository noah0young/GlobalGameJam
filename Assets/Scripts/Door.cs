using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Opens the door when run
    public void openDoor()
    {
        if (open)
        {
            anim.SetTrigger("open");
        }
    }
}
