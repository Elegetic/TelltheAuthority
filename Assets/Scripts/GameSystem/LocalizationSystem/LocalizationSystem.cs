using System.Collections.Generic;
using UnityEngine;
using System.Data;
using ExcelDataReader;
using System.IO;
using TMPro;

[CreateAssetMenu(fileName = "LocalizationSystem", menuName = "LocalizationSystem")]
public class LocalizationSystem : ScriptableObject
{
    public static LocalizationSystem Instance { get; private set; }

    public string jsonFilePath = "localization";
    public string language = "EN_GB";
    private Dictionary<string, string> localizationData = new Dictionary<string, string>();

    public TMP_FontAsset defaultEnglishFont;
    public TMP_FontAsset defaultChineseFont;
    public TMP_FontAsset headerEnglishFont;
    public TMP_FontAsset headerChineseFont;
    public TMP_FontAsset bodyEnglishFont;
    public TMP_FontAsset bodyChineseFont;
    public TMP_FontAsset descriptionEnglishFont;
    public TMP_FontAsset descriptionChineseFont;

    private TMP_FontAsset currentFont;
    private Dictionary<string, TMP_FontAsset> fontMap;
    //private string filePath = "Assets/Resources/Localization.xlsx";

    [System.Serializable]
    public class LocalizationEntry
    {
        public string EN_GB;
        public string ZH_CN;
    }

    [System.Serializable]
    public class LocalizationData
    {
        public List<LocalizationEntry> Sheet1;
    }

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Multiple instances of LocalizationSystem found!");
        }

        InitializeFontMap();
        LoadLocalizationData();
    }

    //Excel sheet load for local
    /*public void LoadLocalizationData()
    {
        localizationData.Clear();

        if (!File.Exists(filePath))
        {
            Debug.LogError("Localization file not found: " + filePath);
            return;
        }

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    string key = reader.GetString(0);
                    string value = reader.GetString(language == "en_gb" ? 0 : 1);
                    localizationData[key] = value;

                    // Debug.Log($"Key: {key}, Value: {value}");
                }
            }
        }

        InitializeFontMap();
        //Debug.Log("Localization data loaded.");
    }

    */

    //json load for WebGL
    public void LoadLocalizationData()
    {
        localizationData.Clear();

        TextAsset localizationJson = Resources.Load<TextAsset>(jsonFilePath);

        if (localizationJson == null)
        {
            Debug.LogError("Localization JSON file not found in Resources: " + jsonFilePath);
            return;
        }

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(localizationJson.text);

        foreach (var entry in loadedData.Sheet1)
        {
            if (language == "EN_GB")
            {
                localizationData[entry.EN_GB] = entry.EN_GB;
            }
            else if (language == "ZH_CN")
            {
                localizationData[entry.EN_GB] = entry.ZH_CN;
            }
        }
    }

    public void LocalizationInitialize()
    {
        language = "EN_GB";

        //Debug.Log("Localization Settings Initialized.");
    }

    private void InitializeFontMap()
    {
        if (language == null)
        {
            Debug.LogError("Language is not set.");
            return;
        }


        fontMap = new Dictionary<string, TMP_FontAsset>();

        if (language == "EN_GB")
        {
            fontMap["default"] = defaultEnglishFont;
            fontMap["header"] = headerEnglishFont;
            fontMap["body"] = bodyEnglishFont;
            fontMap["description"] = descriptionEnglishFont;
        }
        else if (language == "ZH_CN")
        {
            fontMap["default"] = defaultChineseFont;
            fontMap["header"] = headerChineseFont;
            fontMap["body"] = bodyChineseFont;
            fontMap["description"] = descriptionChineseFont;
        }

        if (fontMap == null || fontMap.Count == 0)
        {
            Debug.LogError("Font map initialization failed.");
        }
        else
        {
            //Debug.Log("Font map initialized with " + fontMap.Count + " fonts.");
            foreach (var kvp in fontMap)
            {
                //Debug.Log("Font key: " + kvp.Key + ", Font name: " + (kvp.Value != null ? kvp.Value.name : "null"));
            }
        }
    }


    public string GetLocalizedValue(string key)
    {
        if (localizationData.ContainsKey(key))
        {
            return localizationData[key];
        }
        else
        {
            Debug.LogWarning($"Localization key not found: {key}");
            return key; 
        }
    }

    public void SetLanguage(string newLanguage)
    {
        language = newLanguage;
        LoadLocalizationData();
        InitializeFontMap();

        Debug.Log("Language changed to " + language);
    }

    public TMP_FontAsset GetFont(string key)
    {
        if (fontMap == null)
        {
            Debug.LogError("Font map is not initialized.");
            return null;
        }

        if (fontMap.ContainsKey(key))
        {
            return fontMap[key];
        }
        Debug.LogWarning("Font key not found: " + key + ". Using default font.");
        //Debug.Log("Current Font: " + key);

        return fontMap.ContainsKey("default") ? fontMap["default"] : null;
    }
}