using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Character_Movement : MonoBehaviour
{
    public InputActionReference rotationAction;
    public InputActionReference movementAction;
    public InputActionReference backToZeroAction;
    public float rotationSpeed = 120f;
    public float movementSpeed = 1f;

    private void Start()
    {
        rotationAction.action.Enable();
        movementAction.action.Enable();
        backToZeroAction.action.Enable();
    }

    private void Update()
    {
        Vector2 rotationInput = rotationAction.action.ReadValue<Vector2>();
        Vector2 movementInput = movementAction.action.ReadValue<Vector2>();

        RotateObject(rotationInput);
        MoveObject(movementInput);

        if (backToZeroAction.action.IsPressed()) 
        {
            transform.position = Vector3.zero;  // Reset position
            transform.rotation = Quaternion.identity;  // Reset rotation
        }
    }

    private void RotateObject(Vector2 input)
    {
        float rotationAmount = input.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }

    private void MoveObject(Vector2 input)
    {
        Vector3 movementDirection = new Vector3(input.x, 0f, input.y);
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}