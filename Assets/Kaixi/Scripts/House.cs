using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    
    
    public int houseState;
    public List<House> neighbourHouses= new List<House>();
    public int score;
    public Material defaultMaterial;
    public Material burningMaterial;
    public Material destroiedMaterial;
    
    public float radius = 5;
    Collider[] results = new Collider[10];

    // Start is called before the first frame update
    void Start()
    {
        
        
        houseState = 0;
        AddNeighbour();
    }

    // Update is called once per frame
    void Update()
    {
        Material nowMaterial = GetComponentInChildren<Renderer>().material;
        switch (houseState)
        {
            case 0:
                nowMaterial = defaultMaterial; break;
            case 1:
                nowMaterial = burningMaterial; break;
            case 2:
                nowMaterial = destroiedMaterial; break;
        }
            



    }

 
    private void AddNeighbour()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results);
        for (int i = 0; i < hits; i++)
        {
            if (results[i].TryGetComponent<House>(out House house))
            {
                if(house != this)
                neighbourHouses.Add(house);
            }
        }


    }

    public int getState() {
        return houseState;
    }

    public void setState(int thisState) { 
        houseState = thisState; 
    }

    public void setScore(int thisScore) { 
        score= thisScore;
    }
    public int getScore() {
        return score;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
