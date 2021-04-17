using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip MissionAccepted;
    public AudioClip MissionCompleted;

    // Start is called before the first frame update
    public void Accepted()
    {
        print("Highlight");
        AudioSource.PlayOneShot(MissionAccepted);
    }

    public void Completed()
    {
        print("Pressed");
        AudioSource.PlayOneShot(MissionCompleted);
    }
}
