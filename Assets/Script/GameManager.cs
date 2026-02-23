using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameRunning = false;

    public void StartGame()
    {
        gameRunning = true;
        Debug.Log("Game started!");
    }
}