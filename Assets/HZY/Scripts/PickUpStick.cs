using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PickUpStick : MonoBehaviour
{
    [SerializeField]bool canPickUp;
    public bool isHoldingStick;
    public GameObject stick;

    float ActiveDuration;
    [SerializeField]float timer;

    GameManagement gameManagement;
    public VisualEffect TorchVFX;


    private void Start()
    {
        stick.SetActive(false);
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        ActiveDuration = gameManagement.getTorchFireActiveTime();
    }

    private void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stick.SetActive(true);
                timer = ActiveDuration;
            }
        }


        if (stick.activeSelf)
        { 
            timer-= Time.deltaTime;
            float currentFireSize = (timer / ActiveDuration) * TorchVFX.GetFloat("MaxSize");
            if (timer <= 0)
            {
                stick.SetActive(false);
                TorchVFX.SetFloat("FireSize", 0);
            }
            else{
                TorchVFX.SetFloat("FireSize", currentFireSize);
            }

        }


        isHoldingStick = stick.activeSelf;

    }

    public void SetPickUp()
    {
        canPickUp = !canPickUp;
    }

    //public void getWeather(){
    //  
    //}

}
