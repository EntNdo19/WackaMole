using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloarder : MonoBehaviour
{
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
