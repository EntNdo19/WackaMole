using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelShowInFrontOFButtonPress1 : MonoBehaviour
{
    [Header("Input Action")] [SerializeField]
    private InputActionReference secondayAction;

    [Header("UI/Panel")] [SerializeField] private GameObject panelObject;

    [Header("Positioning of Main Camera in XR Origin")] [SerializeField]
    private Transform playerTransform;

    [SerializeField] private float distanceInFront = 1.5f;
    [SerializeField] private float HeightOffset = 0.0f;

    private void OnEnable()
    {
        secondayAction.action.Enable();
    }
    
    private void OnDisable()
    {
        secondayAction.action.Disable();
    }

    private void Update()
    {
        if (secondayAction.action.WasPressedThisFrame())
        {
            panelObject.SetActive(!panelObject.activeSelf);
        }
        
        if (!panelObject.activeSelf)
        {
            return;
        }

        Vector3 forward = playerTransform.forward;

        forward.y = 0f;
        
        forward.Normalize();

        Vector3 targetPos = playerTransform.position + forward * distanceInFront;

        targetPos.y += HeightOffset;

        panelObject.transform.position = targetPos;
    }
}
