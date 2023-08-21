using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPreferences();
    }

    public void SavePlayerPreferences()
    {
        PlayerPrefs.SetInt("HighScore", GameManager.instance.highScore);
        PlayerPrefs.SetFloat("BGMVolume",GameManager.instance.bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", GameManager.instance.sfxVolume);
    }

    public void LoadPlayerPreferences()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            GameManager.instance.highScore = PlayerPrefs.GetInt("HighScore");
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            GameManager.instance.bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            float newVolume = GameManager.instance.bgmVolume;
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

            // Set the volume to the new volume setting
            audioMixer.SetFloat("MusicVolume", newVolume);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            GameManager.instance.sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            float newVolume = GameManager.instance.sfxVolume;
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

            // Set the volume to the new volume setting
            audioMixer.SetFloat("SFXVolume", newVolume);
        }
    }
}
