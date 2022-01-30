using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartSound : MonoBehaviour
{
    private AudioSource audioSources;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponent<AudioSource>();
        audioSources.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
