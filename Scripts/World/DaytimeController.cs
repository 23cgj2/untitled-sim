using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DaytimeController : MonoBehaviour
{
    const float secondsInDay = 81000f;
    const float phaseLength = 900f;

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

    int oldPhase = 0;

    private void TimeAgents()
    {
      int currentPhase = (int)(time / phaseLength);

      if(oldPhase != currentPhase)
      {
        oldPhase = currentPhase;
        for(int i = 0; i < agents.Count; i++)
        {
          agents[i].Invoke();
        }
      }

    }

}
