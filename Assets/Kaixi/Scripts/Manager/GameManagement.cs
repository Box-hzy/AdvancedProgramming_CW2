using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("Alarm")]
    [SerializeField] bool FireAlarm = false;
    [SerializeField] bool PoliceAlarm = false;

    [Header("Fireman")]
    [SerializeField] [Range(0, 2)] float FiremanPutOffFireSpeed;

    [Header("Torch")]
    [SerializeField] float TorchFireActiveTime;

    [Header("Player")]
    [SerializeField] float PlayerSpeed;

    [Header("Fire")]
    [SerializeField] float smallFireIncreaseSpeed;
    [SerializeField] float largeFireIncreaseSpeed;
    [SerializeField] float oldFireIncreaseSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool getFireAlarm()
    {
        return FireAlarm;
    }

    public void setFireAlarm(bool state)
    {
        FireAlarm = state;
    }


    public bool getPoliceAlarm()
    {
        return PoliceAlarm;
    }

    public void setPoliceAlarm(bool state)
    {
        PoliceAlarm = state;
    }

    public float getTorchFireActiveTime()
    {
        return TorchFireActiveTime;
    }

    public float getFiremanPutOffFireSpeed()
    {
        return FiremanPutOffFireSpeed;
    }

    public float getSmallFireIncreaseSpeed()
    {
        return smallFireIncreaseSpeed;
    }


    public float getLargeFireIncreaseSpeed()
    {
        return largeFireIncreaseSpeed;
    }

    public float getOldFireIncreaseSpeed()
    {
        return oldFireIncreaseSpeed;
    }


}
