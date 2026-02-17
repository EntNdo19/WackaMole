using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float openAngleY = -120f;
    private float rotationSpeed = 3f;

    private bool isOpen;


    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Awake()
    {
        //optional
        closedRotation = transform.rotation;
        Vector3 eulerAnles = closedRotation.eulerAngles;
        eulerAnles.y += openAngleY;
        openRotation = Quaternion.Euler(eulerAnles);
    }

    private void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed);
    }

    public void ToggleDoor()
    {
        Debug.Log("Toggle Door");
        isOpen = !isOpen;
    }
    
    
}
