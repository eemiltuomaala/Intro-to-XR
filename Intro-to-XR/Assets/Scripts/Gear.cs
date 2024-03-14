using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    public float normalizedRotation = 0.0f;
    
void Update()
    {
        float rotationValue = transform.localEulerAngles.y;
        normalizedRotation = normalizeRotation(rotationValue); 

        //Debug.Log("Normalized Rotation: " + normalizedRotation);
    }

    float normalizeRotation(float rotation) 
    {
        rotation = rotation % 360f; // Handle rotations beyond 360 or negative values
        return Mathf.Abs(rotation) / 360f;
    }
}
