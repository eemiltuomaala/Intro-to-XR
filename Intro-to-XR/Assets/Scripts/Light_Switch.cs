using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Light_Switch : MonoBehaviour
{
    public InputActionReference action;
    private Light light;
    private Color[] colors = {Color.white, Color.red, Color.green, Color.blue};
    private int colorIndex = 0;

    private void Start()
    {
        light = GetComponent<Light>();        
        if (light == null)
        {
            Debug.LogError("Light component not found on the GameObject.");
        }
        
        action.action.Enable();
        action.action.started += ctx => CycleColors();
    }

    void CycleColors()
    {
        if (light != null) 
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            light.color = colors[colorIndex];
        }
    }
}