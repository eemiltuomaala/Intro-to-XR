using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(ActionBasedController))]

public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    //public ActionBasedController controller;
    public Hand Hand;

    void Start()
    {
        //controller = GetComponent<ActionBasedController>();
        
        gripAction.action.Enable();
        triggerAction.action.Enable();
    }

    void Update()
    {
        Hand.SetGrip(gripAction.action.ReadValue<float>());
        Hand.SetTrigger(triggerAction.action.ReadValue<float>());
    }
}
