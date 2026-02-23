using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;   // Singleton

    public int currentScore = 0;           // Current score

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want it to persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this to add points
    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score: " + currentScore);
    }

    // Call this to reset score at start of round
    public void ResetScore()
    {
        currentScore = 0;
    }
}