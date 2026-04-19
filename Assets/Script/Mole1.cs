using UnityEngine;

public class Mole1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            FindObjectOfType<GameManager>().AddScore(1);
            Destroy(gameObject);
        }
    }
}