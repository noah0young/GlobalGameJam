using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    public bool bossDefeated;
    public GameObject lever;
    public string[] affectedTags;

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
                for (int j = 0; j < affectedTags.Length; j++)
                {
                    if (transform.GetChild(i).CompareTag(affectedTags[j]))
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
