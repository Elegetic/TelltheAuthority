using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public LocalizationSystem localizationSystem;

    public void SetLanguage(string language)
    {
        localizationSystem.SetLanguage(language);

        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
        //Debug.Log("Language Controller set to: " + language);
    }
}