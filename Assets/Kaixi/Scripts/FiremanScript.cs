using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiremanScript : MonoBehaviour
{
    
    HouseManager houseManager;
    //FireStationManagement fireStationManagement;
    GameManagement gameManagement;
    public GameObject ClosestFireHouse;
    public NavMeshAgent FirefighterAgent;
    public Vector3 FirefighterOriginPostion;
    public FireEngine fireEngineScript;
    public GameObject fireEngine;

    float PutOffFireTime;
    public int state = 0; //0 for go to fire building, 1 for put off fire, 2 for get back to fire engine
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        //fireStationManagement = GameObject.Find("FireStationManagement").GetComponent<FireStationManagement>();
        FirefighterAgent = GetComponent<NavMeshAgent>();
        FirefighterAgent.speed = gameManagement.getFiremanMovingSpeed();
        FirefighterOriginPostion = this.transform.position;
    }

    private void OnEnable()
    {
        fireEngine = transform.parent.gameObject;
        fireEngineScript = fireEngine.GetComponent<FireEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(Vector3.Distance(transform.position, FirefighterOriginPostion));
        switch (state) {
            case 0:
                
                FirefighterAgent.SetDestination(ClosestFireHouse.GetComponent<House>().getCentre());
                if (Vector3.Distance(transform.position, ClosestFireHouse.GetComponent<House>().getCentre()) < 8f) {
                    FirefighterAgent.isStopped = true;
                    state = 1;
                    houseManager.setHouseState(ClosestFireHouse, 2);
                }
                break;
            case 1:
                //Debug.Log("put off fire");
                if (ClosestFireHouse.GetComponent<House>().getIsPutOff()) {
                    houseManager.setHouseState(ClosestFireHouse, 3); //set house state into ruin.
                    state = 2;
                }
                break;
            case 2:
                FirefighterAgent.isStopped = false;
                //Debug.Log("Go Back");
                GoBackToFireEngine();
                break;
        }

    }


    



    void GoBackToFireEngine() {
        FirefighterAgent.SetDestination(FirefighterOriginPostion);
        if (Vector3.Distance(transform.position,FirefighterOriginPostion)<= 1.5f)
        {
            
            transform.SetParent(fireEngine.transform);
            fireEngineScript = fireEngine.GetComponent<FireEngine>();
            fireEngineScript.changeState(2);
            this.gameObject.SetActive(false);
        }
    }

    public void SetFireEnginePostion(Vector3 postion) { 
        FirefighterOriginPostion = postion;

    }

    public int getState() { 
        return state;
    }
}
