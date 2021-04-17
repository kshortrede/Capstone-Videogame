/// <summary>
/// Kenneth Shortrede
/// Audio System --- Trigger for GUI Buttons' sounds
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip onHighlightSound;
    public AudioClip onPressedSound;

    // Start is called before the first frame update
    public void Highlighted()
    {
        AudioSource.PlayOneShot(onHighlightSound);
    }

    public void Selected()
    {
        AudioSource.PlayOneShot(onPressedSound);
    }
}
