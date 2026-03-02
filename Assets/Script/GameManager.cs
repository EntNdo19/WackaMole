using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float roundDuration = 60f;
    public float timeLeft;
    public int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject scoreboardPanel;

    private bool gameRunning = false;

    void Start()
    {
        scoreboardPanel.SetActive(false);
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
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void EndGame()
    {
        gameRunning = false;
        timerText.text = "Time: 0";
        Debug.Log("Game Over!");
    }
}