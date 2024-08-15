using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public AchievementData achievementData;
    public LevelSystem levelSystem;
    public LocalizedText[] achievementNameTexts; 
    public LocalizedText[] achievementDetailTexts;

    public LocalizedText totalAchievementNameText;
    public LocalizedText totalAchievementDetailText; 

    private void Update()
    {
        CheckAchievements();
        UpdateAchievementUI();
    }
    private void CheckAchievements()
    {
        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            if (levelSystem.levels[i].currentLevelScore >= achievementData.achievements[i].requiredScore)
            {
                UnlockAchievement(i);
            }
        }

        if (AllLevelAchievementsUnlocked())
        {
            UnlockTotalAchievement();
        }
    }

    private void UnlockAchievement(int index)
    {
        if (!achievementData.achievements[index].isUnlocked)
        {
            achievementData.achievements[index].isUnlocked = true;
            Debug.Log("Achievement Unlocked: "+achievementData.achievements[index].achievementName);
        }
    }

    private bool AllLevelAchievementsUnlocked()
    {
        foreach (var achievement in achievementData.achievements)
        {
            if (!achievement.isUnlocked) return false;
        }
        return true;
    }

    private void UnlockTotalAchievement()
    {
        if (!achievementData.totalAchievement.isUnlocked)
        {
            achievementData.totalAchievement.isUnlocked = true;
            Debug.Log("Final Achievement Unlocked: " + achievementData.totalAchievement.achievementName);
        }
    }

    private void UpdateAchievementUI()
    {
        for (int i = 0; i < achievementData.achievements.Count; i++)
        {
            if (achievementData.achievements[i].isUnlocked)
            {
                achievementNameTexts[i].key = achievementData.achievements[i].achievementName;
                achievementDetailTexts[i].key = achievementData.achievements[i].description;
            }
            else
            {
                achievementNameTexts[i].key = "???";
                achievementDetailTexts[i].key = "???";
            }
            achievementNameTexts[i].UpdateText();
            achievementDetailTexts[i].UpdateText();
        }

        if (achievementData.totalAchievement.isUnlocked)
        {
            totalAchievementNameText.key = achievementData.totalAchievement.achievementName;
            totalAchievementDetailText.key = achievementData.totalAchievement.description;
        }
        else
        {
            totalAchievementNameText.key = "???";
            totalAchievementDetailText.key = "???";
        }

        totalAchievementNameText.UpdateText();
        totalAchievementDetailText.UpdateText();
    }
}
