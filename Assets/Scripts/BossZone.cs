using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    public bool bossDefeated;
    public GameObject lever;

    private void Update()
    {
        if (bossDefeated)
        {
            lever.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).CompareTag("Pillar"))
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
