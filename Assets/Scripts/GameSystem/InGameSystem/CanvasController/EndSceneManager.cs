using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    public LevelSystem levelSystem;
    public TMP_Text finalScoreText;
    public Button actionButton;
    public TMP_Text actionButtonText;

    private int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = GetCurrentLevelIndex();

        ShowFinalScore();

        if (levelSystem.CanUnlockNextLevel(currentLevelIndex))
        {
            actionButtonText.text = "NEXT LEVEL";
            actionButton.onClick.AddListener(GoToNextLevel);
        }
        else
        {
            actionButtonText.text = "RESTART LEVEL";
            actionButton.onClick.AddListener(RetryLevel);
        }
    }
    private int GetCurrentLevelIndex()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            if (levelSystem.levels[i].sceneName == currentSceneName)
            {
                return i;
            }
        }
        Debug.LogError("Current level index not found!");
        return -1;
    }

    private void ShowFinalScore()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = levelSystem.currentTotalScore.ToString("D2");
        }
    }

    private void GoToNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < levelSystem.levels.Count)
        {
            string nextSceneName = levelSystem.levels[nextLevelIndex].sceneName;
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next level scene name is not set.");
            }
        }
        else
        {
            Debug.LogError("No more levels available.");
        }
    }

    private void RetryLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        levelSystem.currentTotalScore = 0;
        SceneManager.LoadScene(currentSceneName);
    }
}