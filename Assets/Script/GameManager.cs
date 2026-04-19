using UnityEngine;
using TMPro;
using UnityEngine.XR;

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
    public GameObject endGamePanel;
    public GameObject pausePanel;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip startSound;
    public AudioClip gameOverSound;

    public bool gameRunning = false;
    private bool isPaused = false;
    private bool lastSecondaryButtonState = false;

    void Start()
    {
        if (scoreboardPanel != null)
            scoreboardPanel.SetActive(false);

        if (endGamePanel != null)
            endGamePanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        UpdateScoreUI();
        UpdateBestScoreUI();
    }

    void Update()
    {
        if (SecondaryButtonPressedThisFrame())
        {
            TogglePause();
        }

        if (!gameRunning || isPaused)
            return;

        timeLeft -= Time.deltaTime;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0f)
        {
            EndGame();
        }
    }

    bool SecondaryButtonPressedThisFrame()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed))
        {
            if (pressed && !lastSecondaryButtonState)
            {
                lastSecondaryButtonState = true;
                return true;
            }

            lastSecondaryButtonState = pressed;
        }
        else
        {
            lastSecondaryButtonState = false;
        }

        return false;
    }

    public void StartGame()
    {
        score = 0;
        timeLeft = roundDuration;
        gameRunning = true;
        isPaused = false;
        Time.timeScale = 1f;

        if (scoreboardPanel != null)
            scoreboardPanel.SetActive(true);

        if (endGamePanel != null)
            endGamePanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (audioSource && startSound)
            audioSource.PlayOneShot(startSound);

        UpdateScoreUI();

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString();

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
        isPaused = false;
        Time.timeScale = 1f;

        if (timerText != null)
            timerText.text = "Time: 0";

        if (scoreboardPanel != null)
            scoreboardPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (audioSource && gameOverSound)
            audioSource.PlayOneShot(gameOverSound);

        Debug.Log("Game Over!");
    }

    public void TogglePause()
    {
        if (!gameRunning)
            return;

        isPaused = !isPaused;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }
}