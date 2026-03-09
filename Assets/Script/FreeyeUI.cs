using UnityEngine;

public class FreezeUI : MonoBehaviour
{
    void LateUpdate()
    {
        // Keep the panel upright, only allow Y rotation
        Vector3 rotation = transform.eulerAngles;
        rotation.x = 0f;
        rotation.z = 0f;
        transform.eulerAngles = rotation;
    }
}