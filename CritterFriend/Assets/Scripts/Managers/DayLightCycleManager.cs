﻿using UnityEngine;
using System.Collections;

public class DayLightCycleManager : MonoBehaviour {

    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;
    
    float sunInitialIntensity;

    public GameObject[] lights;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        if( currentTimeOfDay > 0.4 && currentTimeOfDay < 0.6)
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(false);
            }
        }else
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(true);
            }
        }

        //if(currentTimeOfDay > 0.6)
        //{
        //    foreach (GameObject g in lights)
        //    {
        //        g.SetActive(true);
        //    }
        //}


        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
