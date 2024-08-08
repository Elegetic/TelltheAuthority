using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public SettingSystem settingSystem;
    public Toggle bgmToggle;
    public Toggle soundFXToggle;
    public Slider bgmSlider;
    public Slider soundFXSlider;

    private List<AudioSource> bgmAudioSources = new List<AudioSource>();
    private List<AudioSource> soundFXAudioSources = new List<AudioSource>();

    void Start()
    {
        GameObject[] bgmObjects = GameObject.FindGameObjectsWithTag("BGM");
        foreach (GameObject obj in bgmObjects)
        {
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                bgmAudioSources.Add(audioSource);
            }
        }

        GameObject[] soundFXObjects = GameObject.FindGameObjectsWithTag("SoundFX");
        foreach (GameObject obj in soundFXObjects)
        {
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                soundFXAudioSources.Add(audioSource);
            }
        }

        bgmToggle.isOn = settingSystem.Flags[0];
        soundFXToggle.isOn = settingSystem.Flags[1];
        bgmSlider.value = settingSystem.Volume[0];
        soundFXSlider.value = settingSystem.Volume[1];

        UpdateBGMState();
        UpdateSoundFXState();

        bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        soundFXToggle.onValueChanged.AddListener(OnSoundFXToggleChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        soundFXSlider.onValueChanged.AddListener(OnSoundFXVolumeChanged);
    }

    void OnBGMToggleChanged(bool isOn)
    {
        settingSystem.SetBGMEnabled(isOn);
        UpdateBGMState();
    }

    void OnSoundFXToggleChanged(bool isOn)
    {
        settingSystem.SetSoundFXEnabled(isOn);
        UpdateSoundFXState();
    }

    void OnBGMVolumeChanged(float value)
    {
        settingSystem.SetBGMVolume((int)value);
        UpdateBGMVolume();
    }

    void OnSoundFXVolumeChanged(float value)
    {
        settingSystem.SetSoundFXVolume((int)value);
        UpdateSoundFXVolume();
    }

    void UpdateBGMState()
    {
        foreach (AudioSource audioSource in bgmAudioSources)
        {
            audioSource.gameObject.SetActive(settingSystem.Flags[0]);
        }
        if (settingSystem.Flags[0])
        {
            UpdateBGMVolume();
        }
    }

    void UpdateSoundFXState()
    {
        foreach (AudioSource audioSource in soundFXAudioSources)
        {
            audioSource.gameObject.SetActive(settingSystem.Flags[1]);
        }
        if (settingSystem.Flags[1])
        {
            UpdateSoundFXVolume();
        }
    }

    void UpdateBGMVolume()
    {
        foreach (AudioSource audioSource in bgmAudioSources)
        {
            audioSource.volume = bgmSlider.value;
        }
    }

    void UpdateSoundFXVolume()
    {
        foreach (AudioSource audioSource in soundFXAudioSources)
        {
            audioSource.volume = soundFXSlider.value;
        }
    }
}