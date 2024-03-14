using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float laserWidth = 0.1f;
    public float maxDistance = 50f;
    public bool active = false;
    public GameObject hitObject = null;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
    }

    void Update()
    {
        if (active)
        {
            RenderLaser();
            lineRenderer.enabled = true;
        }
        else 
        {
            lineRenderer.enabled = false;
        }
    }

    void RenderLaser()
    {
        // Cast a ray from the laser origin
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            // End Laser at the hit point
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            hitObject = hit.collider.gameObject;
        }
        else
        {
            // Laser didn't hit anything
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
        }
    }

    public void Activate()
    {
        active = !active;
    }
}