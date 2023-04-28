using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Animator animator;
    PickUpStick pickUpStick;
    public Transform model;

    Rigidbody rigidbody;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pickUpStick = GetComponent<PickUpStick>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void ChangeAnimationWeight()
    {
        if (pickUpStick.isHoldingStick)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
        
    }


    private void Update()
    {

        ChangeAnimationWeight();

        // Get input axes for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("Run", true);

            if (verticalInput < 0)
            {
                animator.SetBool("ReserveRun", true);
            }
            else
            {
                animator.SetBool("ReserveRun", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Calculate movement direction based on input axes
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize movement direction to prevent faster diagonal movement
        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        //change animation facing
        model.transform.rotation = Quaternion.LookRotation(movementDirection);

        //// Apply movement to transform (CANNOT BE USED IN THIS STATUE)
        //transform.position += movementDirection * speed * Time.deltaTime;

        rigidbody
    }
}
