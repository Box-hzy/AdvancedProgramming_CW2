using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManagement : MonoBehaviour
{
    
    public enum weatherType { 
        Sunny,
        Rainy,
        Foggy
    }

    public weatherType weather;
       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWeather(weatherType thisWeather) { 
        weather = thisWeather;
    }
}
