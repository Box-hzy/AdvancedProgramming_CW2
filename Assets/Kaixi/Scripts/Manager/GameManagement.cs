using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("Alarm")]
    [SerializeField] bool FireAlarm = false;
    [SerializeField] bool PoliceAlarm = false;

    [Header("Torch")]
    [SerializeField] float TorchFireActiveTime_Noramal;
    [SerializeField] float TorchFireActiveTime_Rainy;

    [Header("Player")]
    [SerializeField] float PlayerSpeed;

    [Header("Fire")]
    [SerializeField] float smallFireIncreaseSpeed;
    [SerializeField] float largeFireIncreaseSpeed;
    [SerializeField] float oldFireIncreaseSpeed;
    [SerializeField] float FireSpreadTime;

    [Header("Fireman")]
    [SerializeField][Range(0, 2)] float FiremanPutOffFireSpeed;
    [SerializeField] float FiremanMovingSpeed;
    [SerializeField] float FiremanRange;

    [Header("FireTruck")]
    [SerializeField] float FireTruckSpeed;

    [Header("Policeman")]
    [SerializeField] float PolicePatrolSpeed;
    [SerializeField] float PoliceChaseSpeed;

    [Header("PoliceCar")]
    [SerializeField] float PoliceCarSpeed;


    WeatherManagement weatherManagement;
    

    private void Start()
    {
        weatherManagement = GameObject.Find("WeatherManagement").GetComponent<WeatherManagement>();
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
        switch (weatherManagement.getWeather()) {
            case WeatherManagement.weatherType.Rainy:
                return TorchFireActiveTime_Rainy;
                
        }
        return TorchFireActiveTime_Noramal;
        
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

    public float getFireSpreadTime()
    {
        return FireSpreadTime;
    }

    public float getFiremanMovingSpeed()
    { 
        return FiremanMovingSpeed;
    }

    public float getFireEngineSpeed()
    {
        return FireTruckSpeed;
    }

    public float getPlayerSpeed()
    { 
        return PlayerSpeed;
    }

    public float getPolicePatrolSpeed()
    {
        return PolicePatrolSpeed;
    }

    public float getPoliceChaseSpeed() { 
        return PoliceChaseSpeed;
    }

    public float getPoliceCarSpeed() {
        return PoliceCarSpeed;
    }

    public float getFiremanRange(){
        return FiremanRange;
    }

}
