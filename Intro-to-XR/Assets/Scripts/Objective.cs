using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public Laser laser = null;

    public bool changeColor = false;
    public Color targetColor;
    public float transitionSpeed = 50.0f;

    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    void Update()
    {
        if (laser != null && laser.hitObject == this.gameObject && laser.active)
        {
            objectRenderer.material.color = Color.Lerp(objectRenderer.material.color, targetColor, Time.deltaTime * transitionSpeed);
            changeColor = true;
        }
        else 
        {
            objectRenderer.material.color = Color.Lerp(objectRenderer.material.color, originalColor, Time.deltaTime * transitionSpeed);
            changeColor = false;
        }
    }
}
