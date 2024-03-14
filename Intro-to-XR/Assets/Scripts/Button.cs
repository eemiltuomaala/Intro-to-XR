using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadzone = 0.025f;

    private bool pressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;
    
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!pressed && GetValue() + threshold >= 1) {
            Pressed();
        }

        if (pressed && GetValue() - threshold <= 0){
            Released();
        }
    }

    private float GetValue() {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Math.Abs(value) < deadzone){
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed() {
        pressed = true;
        onPressed.Invoke();
        //Debug.Log("Pressed");
    }

    private void Released() {
        pressed = false;
        onPressed.Invoke();
        //Debug.Log("Released");
    }
}
