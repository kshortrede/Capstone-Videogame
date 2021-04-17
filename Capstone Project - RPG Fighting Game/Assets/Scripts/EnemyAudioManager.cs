using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Oliver Luo
public class EnemyAudioManager : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip healSound;
    public AudioSource enemyAS;

    public void Start()
    {
        enemyAS.clip = attackSound;
    }

    public void PlayAttackSound()
    {
        enemyAS.clip = attackSound;
        enemyAS.Stop();
        enemyAS.Play();
    }

    public void PlayHealSound()
    {
        enemyAS.clip = healSound;
        enemyAS.Stop();
        enemyAS.Play();
    }
}
