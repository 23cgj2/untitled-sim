using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
  Clear,
  Rain,
  HeavyRain,
  RainAndThunder
}

public class WeatherManager : TimeAgent
{
  [Range(0f, 1f)] [SerializeField] float ChanceToChangeWeather = 0.02f;

  WeatherStates currentWeatherState = WeatherStates.Clear;

  [SerializeField] ParticleSystem rainObject;
  [SerializeField] ParticleSystem heavyrainObject;


  private void Start()
  {
    Init();
    onTimeTick += RandomWeatherChangeCheck;
    UpdateWeather();
  }

  public void RandomWeatherChangeCheck()
  {
    if (UnityEngine.Random.value < ChanceToChangeWeather)
    {
      RandomWeatherChange();
    }
  }

  private void RandomWeatherChange()
  {
    WeatherStates newWeatherState = (WeatherStates) UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
    ChangeWeather(newWeatherState);
  }

  private void ChangeWeather(WeatherStates newWeatherState)
  {
    currentWeatherState = newWeatherState;
    Debug.Log(newWeatherState);
    UpdateWeather();
  }

  private void UpdateWeather()
  {
    switch (currentWeatherState)
    {
      case WeatherStates.Clear:
        rainObject.gameObject.SetActive(false);
        heavyrainObject.gameObject.SetActive(false);
        break;
      case WeatherStates.Rain:
        rainObject.gameObject.SetActive(true);
        break;
      case WeatherStates.HeavyRain:
        heavyrainObject.gameObject.SetActive(true);
        break;
      case WeatherStates.RainAndThunder:
        heavyrainObject.gameObject.SetActive(true);
        break;
    }
  }
}
