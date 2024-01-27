using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Break_Out : MonoBehaviour
{
    public InputActionReference action;
    private Vector3 initialPosition;
    public Vector3 targetPosition;
    private bool inInitialPosition = true;

    private void Start()
    {
        initialPosition = transform.position;
        action.action.Enable();
        action.action.started += ctx => Teleport();
    }

    void Teleport()
    {
        if (inInitialPosition) 
        {
            transform.position = targetPosition;
        }
        else
        {
            transform.position = initialPosition;
        }

        inInitialPosition = !inInitialPosition;
    }
}
