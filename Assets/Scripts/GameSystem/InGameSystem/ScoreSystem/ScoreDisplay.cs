using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    public LevelSystem levelSystem;
    public TMP_Text currentScoreText;
    public TMP_Text targetScoreText;
    public GameObject targetScoreGameObject;
    public GameObject bonusImage;

    public AudioSource scoreAudioSource;
    public AudioClip scoreUpClip;
    public AudioClip scoreDownClip;
    public AudioClip partialScoreUpClip;

    private int currentLevelIndex;
    private int previousTotalScore;
    private int consecutiveTwoPoints = 0;

    private Color originalColor;
    private Color greenColor = new Color32(84, 255, 113, 255);
    private Color yellowColor = new Color32(243, 255, 84, 255);
    private Color redColor = new Color32(255, 84, 84, 255);

    void Start()
    {
        currentLevelIndex = GetCurrentLevelIndex();
        previousTotalScore = levelSystem.currentTotalScore;
        UpdateScoreDisplay();

        originalColor = currentScoreText.color;
        bonusImage.SetActive(false);
    }

    void Update()
    {
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (levelSystem != null)
        {
            int currentTotalScore = levelSystem.currentTotalScore;
            int nextLevelTargetScore = levelSystem.GetNextLevelTargetScore(currentLevelIndex);

            if (currentScoreText != null)
            {
                currentScoreText.text = FormatScore(currentTotalScore);
            }

            if (targetScoreText != null)
            {
                targetScoreText.text = nextLevelTargetScore.ToString("D2");
            }

            Image targetScoreImage = targetScoreGameObject.GetComponent<Image>();

            if (currentTotalScore >= nextLevelTargetScore)
            {
                targetScoreImage.color = greenColor;
            }
            else
            {
                targetScoreImage.color = redColor;
            }

            if (currentTotalScore != previousTotalScore)
            {
                int scoreDifference = currentTotalScore - previousTotalScore;

                if (scoreDifference == 1)
                {
                    StartCoroutine(ShowScoreChangeCoroutine(yellowColor, partialScoreUpClip));
                    consecutiveTwoPoints = 0;
                }
                else if (scoreDifference >= 2)
                {
                    StartCoroutine(ShowScoreChangeCoroutine(greenColor, scoreUpClip));
                    consecutiveTwoPoints++;

                    if (consecutiveTwoPoints >= 2)
                    {
                        StartCoroutine(ShowBonusImageCoroutine());
                        levelSystem.levels[currentLevelIndex].currentLevelScore += 1;
                        levelSystem.currentTotalScore += 1;
                    }
                }
                else
                {
                    StartCoroutine(ShowScoreChangeCoroutine(redColor, scoreDownClip));
                    consecutiveTwoPoints = 0;
                }

                previousTotalScore = currentTotalScore;
            }
        }
    }

    private string FormatScore(int score)
    {
        if (score < 0)
        {
            return "-" + Mathf.Abs(score).ToString("D2");
        }
        else
        {
            return score.ToString("D2");
        }
    }

    private IEnumerator ShowScoreChangeCoroutine(Color color, AudioClip clip)
    {
        currentScoreText.color = color;
        if (scoreAudioSource != null)
        {
            scoreAudioSource.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(0.5f);
        currentScoreText.color = originalColor;
    }

    private IEnumerator ShowBonusImageCoroutine()
    {
        bonusImage.SetActive(true);
        yield return new WaitForSeconds(1f);

        float fadeDuration = 0.5f;
        Image bonusImageComponent = bonusImage.GetComponent<Image>();
        Color originalColor = bonusImageComponent.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            bonusImageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - normalizedTime);
            yield return null;
        }
        bonusImageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        bonusImage.SetActive(false);
    }

    private int GetCurrentLevelIndex()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
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
}