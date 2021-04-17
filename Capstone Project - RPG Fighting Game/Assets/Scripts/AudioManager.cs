using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Oliver Luo
public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;

    public void ChangeBGM(AudioClip song)
    {
        BGM.Stop();
        BGM.clip = song;
        BGM.Play();
    }
    public int ChooseRandomBattleTheme()
    {
        int RandomIndex = Random.Range(0, 3);
        return RandomIndex;
    }

    public void StopLoop()
    {
        BGM.loop = false;
    }
}
