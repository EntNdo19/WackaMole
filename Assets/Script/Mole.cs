using UnityEngine;

public class Mole : MonoBehaviour
{
    public float upHeight = 0.5f;
    public float speed = 3f;
    public float visibleTime = 1.5f;
    private Vector3 startPos;
    private bool active = false;

    void Start()
    {
        startPos = transform.localPosition;
    }
    
    

    public void Activate()
    {
        if (active) return;
        active = true;
        StartCoroutine(MoveUpDown());
    }

    System.Collections.IEnumerator MoveUpDown()
    {
        // Move Up
        Vector3 targetUp = startPos + Vector3.up * upHeight;

        while (Vector3.Distance(transform.localPosition, targetUp) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetUp, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(visibleTime);

        // Move Down
        while (Vector3.Distance(transform.localPosition, startPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, speed * Time.deltaTime);
            yield return null;
        }

        active = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.CompareTag("Hammer"))
        {
            GameManager gm = FindObjectOfType<GameManager>();

            if (gm != null && gm.gameRunning)
            {
                gm.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
