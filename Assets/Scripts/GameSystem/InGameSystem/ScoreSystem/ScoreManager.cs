using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int currentScore;
    private int targetScore;
    public Text scoreText;
    public Text resultText;
    public GameObject nextLevelButton;
    public GameObject retryButton;
    public GameObject completePanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public void SetTargetScore(int score)
    {
        targetScore = score;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreUI();
    }

    public void SubtractScore(int score)
    {
        currentScore -= score;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
    }

    public void CompleteLevel()
    {
        completePanel.SetActive(true);
        resultText.text = "Your Score: " + currentScore;
        if (currentScore >= targetScore)
        {
            nextLevelButton.SetActive(true);
            retryButton.SetActive(false);
        }
        else
        {
            nextLevelButton.SetActive(false);
            retryButton.SetActive(true);
        }
    }

    public void SkipLevel()
    {
        currentScore = targetScore;
        CompleteLevel();
    }
}