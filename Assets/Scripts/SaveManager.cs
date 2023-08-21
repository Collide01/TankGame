using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
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
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            GameManager.instance.sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }
}
