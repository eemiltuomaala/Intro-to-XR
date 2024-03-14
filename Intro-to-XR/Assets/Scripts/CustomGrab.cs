using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public List<Transform> nearInteractions = new List<Transform>();
    public Transform grabbedObject = null;
    public Transform interactObject = null;
    public InputActionReference action;
    public InputActionReference doubleRotationAction;
    bool grabbing = false;
    bool doubleRotation = false;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private void Start()
    {
        action.action.Enable();
        doubleRotationAction.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        doubleRotation = doubleRotationAction.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject && !interactObject)
            {
                if (nearInteractions.Count > 0)
                {
                    interactObject = nearInteractions[0];
                } 
                else if (nearObjects.Count > 0)
                {
                    grabbedObject = nearObjects[0];
                }
                else
                {
                    grabbedObject = otherHand.grabbedObject;
                }
                //grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;
            }

            if (grabbedObject)
            {
                // Calculate delta position and rotation
                Vector3 deltaPosition = transform.position - previousPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                if (doubleRotation) {
                    // Double deltaRotation
                    deltaRotation *= deltaRotation;
                    doubleRotation = false;
                }

                // Combine deltas from both hands if both are grabbing
                if (otherHand.grabbedObject == grabbedObject)
                {
                    deltaPosition += otherHand.transform.position - otherHand.previousPosition;
                    deltaRotation = deltaRotation * (otherHand.transform.rotation * Quaternion.Inverse(otherHand.previousRotation));
                }

                // Apply delta position and rotation to grabbed object
                grabbedObject.position += deltaPosition;
                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                // Rotate the object around the controller's origin
                Vector3 objectToController = transform.position - grabbedObject.position;
                objectToController = deltaRotation * objectToController;
                grabbedObject.position = transform.position - objectToController;
            }

            if (interactObject)
            { 
                Gear gear = interactObject.GetComponent<Gear>();
                if (gear != null) {
                    // Calculate angular change based on controller movement
                    Vector3 pivot = interactObject.position; 
                    Vector3 controllerToObject = transform.position - pivot;
                    Vector3 previousControllerToObject = previousPosition - pivot;

                    Quaternion rotationChange =  Quaternion.FromToRotation(previousControllerToObject, controllerToObject);

                    // Restrict rotation to the gear's axis
                    float angle = Vector3.SignedAngle(previousControllerToObject, controllerToObject, gear.rotationAxis);
                    rotationChange = Quaternion.AngleAxis(angle, gear.rotationAxis);

                    // Apply the calculated rotation change to the gear
                    interactObject.rotation = rotationChange * interactObject.rotation;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject || interactObject) 
        {
            grabbedObject = null;
            interactObject = null;
        }
        // Save the current position and rotation
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
        if(t && t.tag.ToLower()=="interactable")
            nearInteractions.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
        if(t && t.tag.ToLower()=="interactable")
            nearInteractions.Remove(t);
    }
}
