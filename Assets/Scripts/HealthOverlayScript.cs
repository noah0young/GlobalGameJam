using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthOverlayScript : MonoBehaviour
{

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.curHealth < 3)
        {
            Heart3.color = new Color(0, 0, 0, 0);
        }
        if (player.curHealth < 2)
        {
            Heart2.color = new Color(0, 0, 0, 0);
        }
        if (player.curHealth < 1)
        {
            Heart1.color = new Color(0, 0, 0, 0);
        }
    }
}
