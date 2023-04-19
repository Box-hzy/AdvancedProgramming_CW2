using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManagement : MonoBehaviour
{
    public int weatherState = 0; // 0 is sunny, 1 is rainy, 2 is foggy
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWeather(int weather) { 
        weatherState= weather;
    }
}
