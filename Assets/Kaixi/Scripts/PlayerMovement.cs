using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        // Get input axes for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input axes
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize movement direction to prevent faster diagonal movement
        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        // Apply movement to transform
        transform.position += movementDirection * speed * Time.deltaTime;
    }
}
