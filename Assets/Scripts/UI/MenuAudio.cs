using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource buttonClick;
    public AudioSource buttonHighlight;

    public void PlayButtonClick()
    {
        buttonClick.Play();
    }

    public void PlayButtonHighlight()
    {
        buttonHighlight.Play();
    }
}
