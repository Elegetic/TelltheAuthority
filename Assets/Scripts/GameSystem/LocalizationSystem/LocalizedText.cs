using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public enum FontKey
    {
        Default,
        Header,
        Body, 
        Description
    }

    public FontKey enumFontKey = FontKey.Default;
    public string key;

    private string fontKey;
    private TMP_Text uiText;
    private LocalizationSystem localizationSystem;

    void Start()
    {
        localizationSystem = LocalizationSystem.Instance;

        if (localizationSystem == null)
        {
            Debug.LogError("LocalizationSystem is not assigned!");
            return;
        }
        localizationSystem.LoadLocalizationData();
        //Debug.Log("LocalizationSystem assigned!");

        uiText = GetComponent<TMP_Text>();

        if (uiText != null)
        {
            key = uiText.text;
            UpdateText();
        }
        else
        {
            Debug.LogError("TMP_Text component not found!");
        }
    }

    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (uiText != null)
        {
            fontKey = enumFontKey.ToString().ToLower();

            uiText.text = localizationSystem.GetLocalizedValue(key);
            TMP_FontAsset tmpFont = localizationSystem.GetFont(fontKey);

            if (tmpFont != null)
            {
                uiText.font = tmpFont;
            }

            //Debug.Log("Current Font: " + (tmpFont != null ? tmpFont.name : "None"));
            //Debug.Log("Current Text: " + uiText.text);
        }
        else
        {
            string fullPath = GetFullPath(this.gameObject);
            Debug.LogError("UI Text component not found on gameObject "+fullPath+"!");
        }
    }

    string GetFullPath(GameObject obj)
    {
        if (obj.transform.parent == null)
        {
            return obj.name;
        }

        return GetFullPath(obj.transform.parent.gameObject) + "/" + obj.name;
    }


}