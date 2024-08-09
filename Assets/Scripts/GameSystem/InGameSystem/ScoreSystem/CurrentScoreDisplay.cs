using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScoreDisplay : MonoBehaviour
{
    public LevelSystem levelSystem;
    public TMP_Text[] levelScoreTexts;
    public TMP_Text totalScoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        for (int i = 0; i < levelScoreTexts.Length && i < levelSystem.levels.Count; i++)
        {
            levelScoreTexts[i].text = levelSystem.levels[i].currentLevelScore.ToString("D2");
        }

        totalScoreText.text = levelSystem.currentTotalScore.ToString("D2");
    }
}