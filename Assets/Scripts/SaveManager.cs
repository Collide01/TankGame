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
        PlayerPrefs.SetFloat("BGMVolume", VolumeManager.Instance.bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", VolumeManager.Instance.sfxVolume);
    }

    public void LoadPlayerPreferences()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            VolumeManager.Instance.bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            VolumeManager.Instance.OnBGMVolumeChange(VolumeManager.Instance.bgmVolume);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            VolumeManager.Instance.sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            VolumeManager.Instance.OnSFXVolumeChange(VolumeManager.Instance.sfxVolume);
        }

    }
}
