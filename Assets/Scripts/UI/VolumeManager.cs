using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public Toggle gameModeCheckbox;
    public Toggle mapModeCheckbox;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            musicVolumeSlider.value = GameManager.instance.bgmVolume * 10;
            OnBGMVolumeChange();
            soundVolumeSlider.value = GameManager.instance.sfxVolume * 10;
            OnSFXVolumeChange();
            gameModeCheckbox.isOn = GameManager.instance.gameMode;
            mapModeCheckbox.isOn = GameManager.instance.mapMode;
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

    public void OnBGMVolumeChange()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.bgmVolume = Mathf.Clamp01(musicVolumeSlider.value / 10);
        }
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(musicVolumeSlider.value / 10);

        // Set the volume to the new volume setting
        audioMixer.SetFloat("MusicVolume", newVolume);

        GameManager.instance.SaveGame();
    }

    public void OnSFXVolumeChange()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.sfxVolume = Mathf.Clamp01(soundVolumeSlider.value / 10);
        }
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(soundVolumeSlider.value / 10);

        // Set the volume to the new volume setting
        audioMixer.SetFloat("SFXVolume", newVolume);

        GameManager.instance.SaveGame();
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
