using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting_Default", menuName = "NewSetting")]
public class SettingSystem : ScriptableObject
{
    //Bool Flags for music and soundFX
    public bool[] Flags = { true, true };
    public float[] Volume = { 1f, 1f };

    public void SettingInitialize()
    {
        Flags = new bool[] { true, true };
        Volume = new float[] { 1f, 1f };

        Debug.Log("Settings Initialized.");
    }

    public void SetBGMVolume(float volume)
    {
        Volume[0] = (float)volume;
        Debug.Log("BGM Volume set to: " + (float)volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        Volume[1] = (float)volume;
        Debug.Log("SoundFX Volume set to: " + (float)volume);
    }

    public void SetBGMEnabled(bool enabled)
    {
        Flags[0] = enabled;
        Debug.Log("BGM Enabled: " + enabled);
    }

    public void SetSoundFXEnabled(bool enabled)
    {
        Flags[1] = enabled;
        Debug.Log("SoundFX Enabled: " + enabled);
    }

    public void ApplySettings()
    {
        GameObject[] bgmObjects = GameObject.FindGameObjectsWithTag("BGM");
        GameObject[] soundFXObjects = GameObject.FindGameObjectsWithTag("SoundFX");

        foreach (GameObject bgm in bgmObjects)
        {
            AudioSource audioSource = bgm.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = Flags[0] ? Volume[0] / 1f : 0f;
                audioSource.mute = !Flags[0];
            }
        }

        foreach (GameObject soundFX in soundFXObjects)
        {
            AudioSource audioSource = soundFX.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = Flags[1] ? Volume[1] / 1f : 0f;
                audioSource.mute = !Flags[1];
            }
        }

        Debug.Log("Settings Applied: BGM and SoundFX configured.");
    }
}
