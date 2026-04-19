using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [Header("Prefabs & Spawn Points")]
    public GameObject molePrefab;
    public Transform[] spawnPoints;

    [Header("Settings")]
    public float spawnInterval = 2f;
    public float moleLifetime = 4f;

    [Header("Spawn Offset (Adjust in Inspector)")]
    public Vector3 spawnOffset = new Vector3(0f, 0.3f, 0f);

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
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            // add score
            FindObjectOfType<GameManager>().AddScore(1);

            // destroy mole
            Destroy(gameObject);
        }
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

        // Instantiate mole with offset
        GameObject mole = Instantiate(
            molePrefab,
            spawnPoint.position + spawnOffset,
            spawnPoint.rotation
        );

        // Fix scale
        mole.transform.localScale = Vector3.one;

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
        return gameManager != null && gameManager.gameRunning;
    }
}