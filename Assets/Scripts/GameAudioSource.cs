using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudioSource : MonoBehaviour
{
    private bool startPlaying;
    public AudioSource source;
    public AudioClip bulletHit;
    public AudioClip powerup;
    public AudioClip tankDeath;
    public AudioClip tankFire;

    // Start is called before the first frame update
    void Start()
    {
        startPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPlaying)
        {
            if (!source.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlayAudio(int sound)
    {
        switch (sound)
        {
            case 0: // Bullet Hit
                source.PlayOneShot(bulletHit);
                startPlaying = true;
                break;
            case 1: // Powerup
                source.PlayOneShot(powerup);
                startPlaying = true;
                break;
            case 2: // Tank Death
                source.PlayOneShot(tankDeath);
                startPlaying = true;
                break;
            case 3: // Tank Fire
                source.PlayOneShot(tankFire);
                startPlaying = true;
                break;
        }
    }
}
