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
        if (pauseAction.action != null)
        {
            pauseAction.action.performed -= OnPausePressed; // avoid double subscription
            pauseAction.action.Enable();
            pauseAction.action.performed += OnPausePressed;
        }
        else
        {
            Debug.LogWarning("Pause Action is NOT assigned!");
        }
    }

    void OnDisable()
    {
        if (pauseAction.action != null)
        {
            pauseAction.action.performed -= OnPausePressed;
            pauseAction.action.Disable();
        }
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
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
        // Just activate the panel, no movement or scaling
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
        SceneManager.LoadScene("MainGame");
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene("StartGame");
    }
}