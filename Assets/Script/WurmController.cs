using UnityEngine;
public enum WurmState
{
    Idle,
    Running,
    Ended
}
[RequireComponent(typeof(Collider))]
public class WurmController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float popHeight = 1f;          // How high the worm pops up
    public float moveSpeed = 1f;          // Speed of up/down movement

    [Header("Score Settings")]
    public int scoreValue = 1;            // Points awarded per hit

    [Header("Optional Sound")]
    public AudioSource hitSound;          // Sound played when hit

    [HideInInspector]
    public WurmState currentState = WurmState.Idle;

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * popHeight;

        // Ensure collider is trigger if using OnTriggerEnter
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    void Update()
    {
        switch (currentState)
        {
            case WurmState.Idle:
            case WurmState.Ended:
                // Stay hidden
                transform.position = startPosition;
                break;

            case WurmState.Running:
                // Smooth up and down movement
                float y = Mathf.PingPong(Time.time * moveSpeed, popHeight);
                transform.position = startPosition + Vector3.up * y;
                break;
        }
    }

    /// <summary>
    /// Call this when the hammer hits the worm
    /// </summary>
    public void Hit()
    {
        if (currentState != WurmState.Running) return;

        // Add score
        ScoreManager.Instance.AddScore(scoreValue);

        // Play hit sound
        if(hitSound != null) hitSound.Play();

        // Optional: reset worm immediately after hit
        // transform.position = startPosition;

        Debug.Log($"{gameObject.name} hit! Score +{scoreValue}");
    }

    /// <summary>
    /// Detect hammer collision
    /// Make sure hammer has tag "Hammer"
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (currentState != WurmState.Running) return;

        if (other.CompareTag("Hammer"))
        {
            Hit();
        }
    }
}
