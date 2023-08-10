using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public Toggle gameModeCheckbox;
    public Toggle mapModeCheckbox;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attempted to create a second audio manager");
            Destroy(this);
        }
    }

    private float ConvertToDecibel(float value)
    {
        float newVolume = value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        return newVolume;
    }

    public void OnBGMVolumeChange(float value)
    {
        bgmVolume = Mathf.Clamp01(value);
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat("MusicVolume", newVolume);
    }

    public void OnSFXVolumeChange(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat("SFXVolume", newVolume);
    }

    public void OnGameModeChange()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameMode = gameModeCheckbox.isOn;
            if (gameModeCheckbox.isOn)
            {
                GameManager.instance.numberOfPlayers = 2;
            }
            else
            {
                GameManager.instance.numberOfPlayers = 1;
            }
        }
    }

    public void OnMapModeChange()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.mapMode = mapModeCheckbox.isOn;
        }
    }
}
