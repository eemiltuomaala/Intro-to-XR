using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Objective objective1 = null;
    public Objective objective2 = null;
    public Objective objective3 = null;
    public bool open = false;

    public float slideDistance = 3.0f; // How far to slide
    public float speed = 1.0f;         // Speed of the movement
    private Vector3 startPosition;     // Original position of the door

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (objective1.changeColor && objective2.changeColor && objective3.changeColor) {
            open = true;
        }
        if (open)
        {
            if (transform.position.y > startPosition.y - slideDistance)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }
}