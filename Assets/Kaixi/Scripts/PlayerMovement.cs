using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Animator animator;
    PickUpStick pickUpStick;
    public Transform model;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Rigidbody rig;
    GameManagement gameManagement;
    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        animator = GetComponentInChildren<Animator>();
        pickUpStick = GetComponent<PickUpStick>();
        rig = GetComponent<Rigidbody>();
        speed = gameManagement.getPlayerSpeed();
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

    //for debug use
    //public GameObject go;
    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, -transform.up, out hit, 100))
    //    {
    //        go = hit.transform.gameObject;
    //    }

    //}

    private void Update()
    {

        ChangeAnimationWeight();

        // Get input axes for horizontal and vertical movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("Run", true);

            if (verticalInput < 0)
            {
                animator.SetBool("ReverseRun", true);
            }
            else
            {
                animator.SetBool("ReverseRun", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Calculate movement direction based on input axes
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        //ector3 movementDirection = (transform.forward * horizontalInput + transform.right * verticalInput).normalized;

        // Normalize movement direction to prevent faster diagonal movement
        //if (movementDirection.magnitude > 1f)
        //{
        //    movementDirection.Normalize();
        //}

        //change animation facing
        if (movementDirection.magnitude >= 0.1f)
        {
            float facingAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, facingAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //model.transform.rotation = Quaternion.LookRotation(movementDirection);

            //// Apply movement to transform (CANNOT BE USED IN THIS STATUE)
            //transform.position += movementDirection * speed * Time.deltaTime;
            Vector3 moveDir = Quaternion.Euler(0, facingAngle, 0) * Vector3.forward;
            //rig.velocity = movementDirection * speed * Time.deltaTime;
            rig.velocity = moveDir.normalized * speed * Time.deltaTime;
            //Debug.Log(rig.velocity);
        }



    }
}
