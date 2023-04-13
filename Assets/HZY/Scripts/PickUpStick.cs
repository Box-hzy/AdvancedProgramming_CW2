using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStick : MonoBehaviour
{
    [SerializeField]bool canPickUp;
    public bool isHoldingStick;
    public GameObject stick;

    public float ActiveTime;
    [SerializeField]float timer;


    private void Start()
    {
        stick.SetActive(false);
    }

    private void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stick.SetActive(true);
                timer = ActiveTime;
            }
        }


        if (stick.activeSelf)
        { 
            timer-= Time.deltaTime;
            if (timer < 0)
            {
                stick.SetActive(false);
            }
        }


        isHoldingStick = stick.activeSelf;

    }

    public void SetPickUp()
    {
        canPickUp = !canPickUp;
    }

}
