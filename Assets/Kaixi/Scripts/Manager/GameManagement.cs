using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    bool FireAlarm = false;
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
}
