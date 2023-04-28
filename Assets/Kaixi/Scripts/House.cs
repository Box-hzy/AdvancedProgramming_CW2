using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class House : MonoBehaviour
{


    public int houseState;
    public List<House> neighbourHouses = new List<House>();
    public int score;
    public Material defaultMaterial;
    public Material burningMaterial;
    public Material destroiedMaterial;
    public GameObject vfx;
    //public Transform escapePoint;
    public float radius = 10;    //raycast radius
    Collider[] results = new Collider[10];
    [SerializeField] LayerMask layerMask;
    float RecoverTime = 180;
    float timer;
    Vector3 centrePoint; //transform.position is based on Pivot point, however the pivot point of the model is too faraway
    float FireTimer = 0;
    VisualEffect fireVFX;

    [Header("HouseBuring")]
    float FireSpeed;

    [Header("Put Off Fire")]
    float putoffFireSpeed;
    float putoffFireTime = 0;
    bool isPutOff = false;
    GameManagement gameManagement;



    // Start is called before the first frame update
    void Start()
    {

        houseState = 0;
        layerMask = 1 << 10;
        defaultMaterial = GetComponent<MeshRenderer>().material;
        gameObject.AddComponent<NavMeshObstacle>().carving = true;
        centrePoint = GetComponent<MeshRenderer>().bounds.center;
        AddNeighbour();

        //instantiate vfx component
        Instantiate(vfx, centrePoint, Quaternion.identity, transform);
        fireVFX = GetComponentInChildren<VisualEffect>();

        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        FireSpeed = GetFireSpeed();
        putoffFireSpeed = gameManagement.getFiremanPutOffFireSpeed();

        //putoffFire = fireVFX.GetFloat("MaxSize");
        //SetEscapePoint();
    }

    float GetFireSpeed()
    {
        if (transform.parent.CompareTag("OldHouse"))
        {
            return gameManagement.getOldFireIncreaseSpeed();
        }
        else if (transform.parent.CompareTag("SmallHouse"))
        {
            return gameManagement.getSmallFireIncreaseSpeed();
        }
        else if (transform.parent.CompareTag("BigHouse"))
        {
            return gameManagement.getLargeFireIncreaseSpeed();
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (houseState == 1)
        { //fire will get big with the time increase;

            FireTimer += Time.deltaTime;
            float CurrentFireSize = FireTimer * FireSpeed;
            if (CurrentFireSize <= fireVFX.GetFloat("MaxSize"))
            {
                fireVFX.SetFloat("FireSize", CurrentFireSize);
            }
            else
            {
                fireVFX.SetFloat("FireSize", fireVFX.GetFloat("MaxSize"));
                //fireVFX.SetFloat("FireSize", 50);
            }

            if (CurrentFireSize >= 10) {
                BurnNeighbour();
            }



        }
        if (houseState == 2)
        {

            putoffFireTime += Time.deltaTime;
            float CurrentFireSize = fireVFX.GetFloat("FireSize") - putoffFireSpeed * putoffFireTime;


            if (CurrentFireSize > 0)
            {
                fireVFX.SetFloat("FireSize", CurrentFireSize);
            }
            else
            {
                fireVFX.SetFloat("FireSize", 0);
                putoffFireTime = 0;
                houseState = 3;
                isPutOff = true;
            }


        }



        if (houseState == 3)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                setState(0);
            }
        }


    }




    private void AddNeighbour()
    {
        int hits = Physics.OverlapSphereNonAlloc(centrePoint, radius, results, layerMask);
        for (int i = 0; i < hits; i++)
        {
            //Debug.Log("111");
            if (results[i].TryGetComponent<House>(out House house))
            {

                if (house != this)
                    neighbourHouses.Add(house);
            }
        }


    }

    void BurnNeighbour() {
        foreach (House house in neighbourHouses) {
            if (house.getState() == 0) {
                house.setState(1);
            } 
        }
    }

    public int getState()
    {
        return houseState;
    }

    public void setState(int thisState)
    {
        houseState = thisState;

        switch (houseState)
        {
            case 0://original state
                GetComponent<Renderer>().material = defaultMaterial;
                break;
            case 1://fire is burning
                GetComponent<Renderer>().material = burningMaterial;
                isPutOff = false;
                break;
            case 2:// fireman is putting off the fire
                break;
            case 3://house into ruin
                GetComponent<Renderer>().material = destroiedMaterial;
                timer = RecoverTime;

                break;
        }
    }

    public void setScore(int thisScore)
    {
        score = thisScore;
    }
    public int getScore()
    {
        return score;
    }

    public Vector3 getCentre()
    {
        return centrePoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centrePoint, radius);
    }

    public bool getIsPutOff()
    {
        return isPutOff;
    }
}
