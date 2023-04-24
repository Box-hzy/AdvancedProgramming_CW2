using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public bool FireAlarm = false;
    public bool PoliceAlarm = false; 
    
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
}
