using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LeveltoStart : MonoBehaviour
{
    public Button returnButton;
    public Button quitButton;
    public LevelSystem levelSystem;
    public Animator animator;

    void Start()
    {
        if (returnButton != null)
        {
            returnButton.onClick.AddListener(OnReturnButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);

        }
    }

    private void OnReturnButtonClicked()
    {
        StartCoroutine(ReturnToStartScene());
    }

    private void OnQuitButtonClicked()
    {
        ResetCurrentLevelScore();
        StartCoroutine(ReturnToStartScene());
    }

    private void ResetCurrentLevelScore()
    {
        int currentLevelIndex = GetCurrentLevelIndex();
        if (currentLevelIndex >= 0 && currentLevelIndex < levelSystem.levels.Count)
        {
            levelSystem.currentTotalScore -= levelSystem.levels[currentLevelIndex].currentLevelScore;

            levelSystem.levels[currentLevelIndex].currentLevelScore = 0;
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

    IEnumerator ReturnToStartScene()
    {
        //Debug.Log("Coroutine Started! ");
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("Fade_In");
            //Debug.Log("Animator Played! ");

            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Transition Animator is null! ");
        };
        yield return new WaitForSeconds(1);
    }
}
