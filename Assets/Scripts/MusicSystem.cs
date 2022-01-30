using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioSource footsteps;
    public AudioClip[] humanFootsteps;
    public AudioClip[] monsterFootsteps;
    public AudioSource vocals;
    public AudioClip[] humanVocals;
    public AudioClip[] monsterVocals;
    public AudioSource jumpOrPunch;
    public AudioClip jumpUp;
    public AudioClip jumpDown;
    public AudioClip[] punchSounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFootsteps(bool isHuman)
    {
        if (isHuman)
        {
            footsteps.clip = humanFootsteps[Random.Range(0, humanFootsteps.Length)];
        }
        else
        {
            footsteps.clip = monsterFootsteps[Random.Range(0, monsterFootsteps.Length)];
        }
        footsteps.Play();
    }

    public void playVocal(bool isHuman)
    {
        if (isHuman)
        {
            vocals.clip = humanVocals[Random.Range(0, humanVocals.Length)];
        }
        else
        {
            vocals.clip = monsterVocals[Random.Range(0, monsterVocals.Length)];
        }
        vocals.Play();
    }

    public void playPunch()
    {
        jumpOrPunch.clip = punchSounds[Random.Range(0, punchSounds.Length)];
        jumpOrPunch.Play();
    }

    public void playJumpUp()
    {
        jumpOrPunch.clip = jumpUp;
        jumpOrPunch.Play();
    }

    public void playJumpLand()
    {
        jumpOrPunch.clip = jumpDown;
        jumpOrPunch.Play();
    }
}
