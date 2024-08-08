using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting_Default", menuName = "NewSetting")]
public class SettingSystem : ScriptableObject
{
    //Bool Flags for music and soundFX
    public bool[] Flags = { true, true };
    public int[] Volume = { 1, 1 };

    public void SettingInitialize()
    {
        Flags = new bool[] { true, true };
        Volume = new int[] { 1, 1 };

        Debug.Log("Settings Initialized.");
    }

    public void SetBGMVolume(int volume)
    {
        Volume[0] = volume;
        Debug.Log("BGM Volume set to: " + volume);
    }

    public void SetSoundFXVolume(int volume)
    {
        Volume[1] = volume;
        Debug.Log("SoundFX Volume set to: " + volume);
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
}
