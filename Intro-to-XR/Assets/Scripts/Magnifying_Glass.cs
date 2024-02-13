using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnifying_Glass : MonoBehaviour
{
    public Transform mainCamera;

    void Update()
    {
        transform.LookAt(mainCamera, transform.parent.up);
    }
}
