using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    HouseManager HouseManager;
    public Material BurningMaterial;
    public float InteractDistance;
    // Start is called before the first frame update
    void Start()
    {
        HouseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { //player press E
            if (HouseManager.getMinDistance()<=InteractDistance) {
                burnHouse();
            }
            
        }
    }


    void burnHouse() { 
        

        //code for buring house take time.
        
        HouseManager.getClosestHouse().GetComponentInChildren<Renderer>().material = BurningMaterial;
        //replace later
    }
}
