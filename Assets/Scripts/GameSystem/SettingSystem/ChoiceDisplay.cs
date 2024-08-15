using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDisplay : MonoBehaviour
{
    public GameObject zh_cnBackground;
    public GameObject en_gbBackground;
    public LocalizationSystem localizationSystem;

    void Update()
    {
        if (localizationSystem.language == "ZH_CN")
        {
            zh_cnBackground.SetActive(true);
            en_gbBackground.SetActive(false);
        }
        else if(localizationSystem.language == "EN_GB")
        {
            zh_cnBackground.SetActive(false);
            en_gbBackground.SetActive(true);
        }
    }
}
