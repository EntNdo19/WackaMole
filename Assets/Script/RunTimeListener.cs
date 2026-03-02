using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
public class RunTimeListener : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor Socket;


    private void Awake()
    {
        Socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        if (Socket == null) return;
        
        Socket.selectEntered.AddListener(OnObjectInserted);
        Socket.selectExited.AddListener(OnObjectRemoved);
    }
    
    private void OnDisable()
    {
        if (Socket == null) return;

        Socket.selectEntered.RemoveListener(OnObjectInserted);
        Socket.selectExited.RemoveListener(OnObjectRemoved);
    }

    private void OnObjectRemoved(SelectExitEventArgs arg0)
    {
        Debug.Log("socket removed"+ arg0.interactableObject.transform.name);
    }


    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        Debug.Log("socket inserted");
    }
    
}
