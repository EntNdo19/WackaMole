using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;   // Singleton

    public int currentScore = 0;
    public int bestScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load best score
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void Start()
    {
        UpdateUI();
    }

    // Add points
    public void AddScore(int amount)
    {
        currentScore += amount;

        // Check for new best score
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    // Reset score at start of round
    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (bestScoreText != null)
            bestScoreText.text = "Best: " + bestScore;
    }
}