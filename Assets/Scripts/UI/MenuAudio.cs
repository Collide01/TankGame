using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource buttonSounds;
    public AudioClip buttonClick;
    public AudioClip buttonHighlight;

    public void PlayButtonClick()
    {
        buttonSounds.PlayOneShot(buttonClick);
    }

    public void PlayButtonHighlight()
    {
        buttonSounds.PlayOneShot(buttonHighlight);
    }
}
