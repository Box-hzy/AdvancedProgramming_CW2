using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int houseState;
    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        houseState = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
