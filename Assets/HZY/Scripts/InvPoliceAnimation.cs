using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvPoliceAnimation : MonoBehaviour
{
    PolicemanInvInCar policemanInvInCar;


    // Start is called before the first frame update
    void Start()
    {
        policemanInvInCar = GetComponentInParent<PolicemanInvInCar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStateEnter()
    { 
    
    }

    void OnStateExit()
    { 
    
    }
}
