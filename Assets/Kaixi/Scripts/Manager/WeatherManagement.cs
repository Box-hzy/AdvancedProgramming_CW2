using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManagement : MonoBehaviour
{
    public static WeatherManagement Instance; 

    public enum weatherType { 
        Sunny,
        Rainy,
        Foggy
    }

    public weatherType currentWeather;

    public ParticleSystem rain;
    public Vector2 durationRange = new Vector2(60,180);
    [SerializeField]private float timer;

    private void Awake()
    {
        Instance = this;
    }

  
    void Start()
    {
        timer = RandomTimer();
    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            changeWeather(RandomState()); 
        }
    }



    weatherType RandomState()
    {  
        int randomIndex = Random.Range(0, 3);
        weatherType weatherType = (weatherType)Random.Range(0, 3);
        return weatherType;
    }

    public void changeWeather(weatherType thisWeather) {

        OnExitState();
        currentWeather = thisWeather;
        OnEnterState();
    }

    void OnExitState()
    {
        switch (currentWeather)
        {
            case weatherType.Sunny:
                break;
            case weatherType.Rainy:
                rain.Stop();
                break;
            case weatherType.Foggy:
                break;
            default:
                break;
        }
    }

    float RandomTimer()
    {
        return Random.Range(durationRange.x, durationRange.y);
    }

    void OnEnterState()
    {
        timer = RandomTimer();

        switch (currentWeather)
        {
            case weatherType.Sunny:
                break;
            case weatherType.Rainy:
                rain.Play();
                break;
            case weatherType.Foggy:
                break;
            default:
                break;
        }
    }
}
