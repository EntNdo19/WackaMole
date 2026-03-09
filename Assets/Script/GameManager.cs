using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float roundDuration = 60f;
    public float timeLeft;

    [Header("Score")]
    public int score;
    public int bestScore;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestScoreText;
    public GameObject scoreboardPanel;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip startSound;
    public AudioClip gameOverSound;

    public bool gameRunning = false;

    void Start()
    {
        scoreboardPanel.SetActive(false);

        // Load saved best score
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        UpdateScoreUI();
        UpdateBestScoreUI();
    }

    void Update()
    {
        if (!gameRunning) return;

        timeLeft -= Time.deltaTime;

        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0f)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        score = 0;
        timeLeft = roundDuration;
        gameRunning = true;

        scoreboardPanel.SetActive(true);

        if (audioSource && startSound)
            audioSource.PlayOneShot(startSound);

        UpdateScoreUI();

        Debug.Log("Game started");
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (audioSource && hitSound)
            audioSource.PlayOneShot(hitSound);

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            UpdateBestScoreUI();
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    void UpdateBestScoreUI()
    {
        if (bestScoreText != null)
            bestScoreText.text = "Best: " + bestScore.ToString();
    }

    public void EndGame()
    {
        gameRunning = false;

        timerText.text = "Time: 0";

        if (audioSource && gameOverSound)
            audioSource.PlayOneShot(gameOverSound);

        Debug.Log("Game Over!");
    }
}