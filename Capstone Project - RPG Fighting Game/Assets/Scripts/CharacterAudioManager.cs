using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Oliver Luo
public class CharacterAudioManager : MonoBehaviour
{
    public AudioClip[] attackSounds;
    public AudioClip healSound;
    public AudioSource playerAS;

    public void Start()
    {
        playerAS.clip = attackSounds[0];
    }

    public void PlayAttackSound()
    {
        playerAS.clip = attackSounds[chooseRandomSound(attackSounds)];
        playerAS.Stop();
        playerAS.Play();
    }

    public void PlayHealSound()
    {
        playerAS.clip = healSound;
        playerAS.Stop();
        playerAS.Play();
    }

    public int chooseRandomSound(AudioClip[] sounds)
    {
        int choice = Random.Range(0, sounds.Length);
        return choice;
    }
}
