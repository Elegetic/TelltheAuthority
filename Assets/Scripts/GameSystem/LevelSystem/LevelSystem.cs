using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelSystem", menuName = "LevelSystem")]
public class LevelSystem : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public string levelContent;
        public Sprite levelImage;
        public string sceneName;
        public int targetScore;
        public int currentLevelScore;
    }

    public List<LevelData> levels; 
    public int defaultLevelIndex = 0;
    public int currentTotalScore;

    public LevelData GetLevelData(int index)
    {
        if (index >= 0 && index < levels.Count)
        {
            return levels[index];
        }
        return null;
    }

    public int GetNextLevelTargetScore(int currentLevelIndex)
    {
        if (currentLevelIndex < 0 || currentLevelIndex >= levels.Count - 1)
        {
            return 0;
        }

        return levels[currentLevelIndex + 1].targetScore;
    }

    public bool CanUnlockCurrentLevel(int currentLevelIndex)
    {
        if (currentLevelIndex < 0 || currentLevelIndex > levels.Count)
        {
            return false;
        }

        int currentTargetScore = levels[currentLevelIndex].targetScore;

        //Debug.Log("Current Target Score: " + currentTargetScore);
        //Debug.Log("Current Score: " + currentTotalScore);

        return currentTotalScore >= currentTargetScore;
    }

    public bool CanUnlockNextLevel(int currentLevelIndex)
    {
        if (currentLevelIndex < 0 || currentLevelIndex > levels.Count)
        {
            return false;
        }

        int nextLevelTargetScore = levels[currentLevelIndex + 1].targetScore;
        return currentTotalScore >= nextLevelTargetScore;
    }


    public void ClearCurrentLevelScore(int currentLevelIndex)
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levels.Count)
        {
            LevelData levelData = levels[currentLevelIndex];
            currentTotalScore -= levelData.currentLevelScore;
            levelData.currentLevelScore = 0;
        }
    }
}