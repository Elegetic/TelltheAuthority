using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    public LocalizationSystem localizationSystem;
    public Button zh_cnButton;
    public Button en_gbButton;

    void Start()
    {
        zh_cnButton.onClick.AddListener(() => localizationSystem.SetLanguage("zh_cn"));
        en_gbButton.onClick.AddListener(() => localizationSystem.SetLanguage("en_gb"));
    }
}
