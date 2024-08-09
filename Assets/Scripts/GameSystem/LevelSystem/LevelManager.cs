using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public LevelSystem levelSystem;
    public TMP_Text levelNameText;
    public TMP_Text levelContentText;
    public Image levelImage;
    public List<Button> levelButtons;
    public Button startButton;
    public Animator animator;
    public GameObject imageSwitch;
    public GameObject unlockMessage;

    public int currentLevelIndex;
    private LocalizedText localizedNameText;
    private LocalizedText localizedContentText;

    void Start()
    {
        animator.enabled = false;

        if (levelSystem == null || levelSystem.levels == null || levelSystem.levels.Count == 0)
        {
            Debug.LogError("Level data is not properly set up.");
            return;
        }

        localizedNameText = levelNameText.GetComponent<LocalizedText>();
        localizedContentText = levelContentText.GetComponent<LocalizedText>();

        currentLevelIndex = levelSystem.defaultLevelIndex;
        UpdateLevelUI(currentLevelIndex);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i; 
            levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(index));
        }


        CheckUnlockStatus();
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void UpdateLevelUI(int index)
    {
        LevelSystem.LevelData levelData = levelSystem.GetLevelData(index);
        if (levelData != null)
        {
            localizedNameText.key = levelData.levelName;
            localizedContentText.key = levelData.levelContent;
            localizedNameText.UpdateText();
            localizedContentText.UpdateText();

            if (levelImage != null)
            {
                levelImage.sprite = levelData.levelImage;
            }

            CheckUnlockStatus();
        }
    }

    private void CheckUnlockStatus()
    {
        bool canUnlock = levelSystem.CanUnlockCurrentLevel(currentLevelIndex);
        startButton.interactable = canUnlock;
        unlockMessage.SetActive(!canUnlock);
    }

    private void OnLevelButtonClicked(int index)
    {
        ClearLevelScore(index);

        currentLevelIndex = index;
        UpdateLevelUI(index);
    }

    private void OnStartButtonClicked()
    {
        LevelSystem.LevelData levelData = levelSystem.GetLevelData(currentLevelIndex);
        if (levelData != null && !string.IsNullOrEmpty(levelData.sceneName))
        {
            StartCoroutine(LoadScene(levelData.sceneName));
        }
        else
        {
            Debug.LogError("Scene name is not set for the selected level.");
        }
    }

    private void ClearLevelScore(int index)
    {
        LevelSystem.LevelData levelData = levelSystem.GetLevelData(index);
        if (levelData != null)
        {
            levelSystem.currentTotalScore -= levelData.currentLevelScore;

            levelData.currentLevelScore = 0;
        }
    }

    IEnumerator LoadScene(string index)
    {
        animator.enabled = true;
        animator.Play("Fade_In");

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
    }

}
