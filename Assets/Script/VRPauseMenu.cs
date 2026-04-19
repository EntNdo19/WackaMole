using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRPauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenuUI;

    [Header("VR Input")]
    public InputActionProperty pauseAction;

    private bool isPaused = false;

    void OnEnable()
    {
        if (pauseAction.action == null)
        {
            Debug.LogWarning("Pause Action is NOT assigned!");
            return;
        }

        pauseAction.action.Enable();
        pauseAction.action.performed += OnPausePressed;
        Debug.Log("Pause action enabled: " + pauseAction.action.name);
    }

    void OnDisable()
    {
        if (pauseAction.action == null) return;

        pauseAction.action.performed -= OnPausePressed;
        pauseAction.action.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        Debug.Log("Pause pressed!");
        TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartGame");
    }
}