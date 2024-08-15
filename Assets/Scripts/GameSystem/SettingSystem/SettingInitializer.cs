using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInitializer : MonoBehaviour
{
    public SettingSystem settingSystem;

    void Start()
    {
        if (settingSystem != null)
        {
            settingSystem.ApplySettings();
        }
        else
        {
            Debug.LogError("SettingSystem not assigned!");
        }
    }
}
