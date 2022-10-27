using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DaytimeController : MonoBehaviour
{
    const float secondsInDay = 81000f;
    const float phaseLength = 900f;
    const float phasesInDay = 90;

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    const float startTime = 19800f;
    float time = startTime;
    const float noon = 43200f;
    [SerializeField] float timeScale = 120f;

    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;
    private int days;
    private string marker = "AM";

    List<TimeAgent> agents;

    private void Awake()
    {
      agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
      agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
      agents.Remove(timeAgent);
    }

    float Hours
    {
      get { return time / 3600f; }
    }

    float Minutes
    {
      get { return time % 3600f / 60f; }
    }

    private void Update()
    {
      time += Time.deltaTime * timeScale;
      TimeValueCalculation();
      DayLight();

      if(time >= noon)
      {
        marker = "PM";
      }
      if(time > secondsInDay)
      {
        marker = "AM";
        NextDay();
      }
      
      TimeAgents();

      if(Input.GetKeyDown(KeyCode.T))
      {
        SkipTime(hours: 4);
      }
    }

    private int CalculatePhase()
    {
      return (int)(time / phaseLength) + (int)(days * phasesInDay);
    }

    private void NextDay()
    {
      time = startTime;
      days += 1;
    }

    private void TimeValueCalculation()
    {
      int hh = (int)Hours;
      int mm = (int)Minutes;
      text.text = "Day " + days + " " +
        hh.ToString("00") + ":" + mm.ToString("00") + marker;
    }

    private void DayLight()
    {
      float v = nightTimeCurve.Evaluate(Hours);
      Color c = Color.Lerp(dayLightColor, nightLightColor, v);
      globalLight.color = c;
    }

    int oldPhase = -1;

    private void TimeAgents()
    {
      if(oldPhase == -1)
      {
        oldPhase = CalculatePhase();
      }

      int currentPhase = CalculatePhase();

      while (oldPhase < currentPhase)
      {
        oldPhase += 1;
        for(int i = 0; i < agents.Count; i++)
        {
          agents[i].Invoke();
        }
      }

    }

    public void SkipTime(float seconds = 0, float minute = 0, float hours = 0)
    {
      // float timeToSkip = seconds;
      // timeToSkip += minute * 60f;
      // timeToSkip += hours * 3600f;

      // time += timeToSkip;
      NextDay();
    }

}
