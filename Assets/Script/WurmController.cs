using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [Header("Prefabs & Spawn Points")]
    public GameObject molePrefab;
    public Transform[] spawnPoints;

    [Header("Settings")]
    public float spawnInterval = 2f;
    public float moleLifetime = 4f;

    private GameManager gameManager;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
            return;
        }

        // Start spawning moles repeatedly
        InvokeRepeating(nameof(SpawnMole), 1f, spawnInterval);
    }

    void SpawnMole()
    {
        if (!IsGameRunning()) return;

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned for moles!");
            return;
        }

        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate mole
        GameObject mole = Instantiate(molePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        // Reset local position (optional)
        mole.transform.localPosition = Vector3.zero;

        // Activate mole script
        Mole moleScript = mole.GetComponent<Mole>();
        if (moleScript != null)
        {
            moleScript.Activate();
        }
        else
        {
            Debug.LogWarning("Mole prefab does not have a Mole script attached!");
        }

        // Destroy mole after lifetime
        Destroy(mole, moleLifetime);
    }

    bool IsGameRunning()
    {
        // Using a public property in GameManager instead of reflection
        return gameManager != null && gameManager.gameRunning;
    }
}