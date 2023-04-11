using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    HouseManager HouseManager;
    public Material BurningMaterial;
    public GameObject closestHouse;
    public float InteractDistance;
    GameManagement gameManagement;

    public float interactTime = 0.0f;
    public float interactThreshold = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        HouseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        closestHouse = HouseManager.getClosestHouse(this.gameObject);
        
        //Debug.Log(this.gameObject);
        if (Input.GetKey(KeyCode.E))
        { //player press E
            interactTime += Time.deltaTime;
            if (interactTime >= interactThreshold)
            {
                burnHouse();
            }
        }
        else { 
            interactTime= 0.0f;
        }

       // Debug.Log(HouseManager.getMinDistance(this.gameObject));
    }


    void burnHouse() {
        
        
        if (HouseManager.getMinDistance(this.gameObject) <= InteractDistance)
        {
            GameObject closetHouse = HouseManager.getClosestHouse(this.gameObject);
          

            closetHouse.GetComponentInChildren<Renderer>().material = BurningMaterial;
            HouseManager.setHouseState(closetHouse, 1);
            gameManagement.setFireAlarm(true);
        }
       
     
        //replace later
    }
}
