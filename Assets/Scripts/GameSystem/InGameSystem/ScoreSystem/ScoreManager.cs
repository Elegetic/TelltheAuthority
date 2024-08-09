using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public LevelSystem levelSystem;

    void Start()
    {
        LoadGameData();
    }

    void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("CurrentTotalScore", levelSystem.currentTotalScore);

        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            string key = "LevelScore_" + i;
            PlayerPrefs.SetInt(key, levelSystem.levels[i].currentLevelScore);
        }

        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        levelSystem.currentTotalScore = PlayerPrefs.GetInt("CurrentTotalScore", 0);

        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            string key = "LevelScore_" + i;
            levelSystem.levels[i].currentLevelScore = PlayerPrefs.GetInt(key, 0);
        }
    }

    public void ClearGameData()
    {
        PlayerPrefs.DeleteKey("CurrentTotalScore");

        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            string key = "LevelScore_" + i;
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.Save();
    }
}