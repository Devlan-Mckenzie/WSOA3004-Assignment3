﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoomTimer : MonoBehaviour
{

    public GameObject LightsOn;
    public GameObject LightsOff;

    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            LightsOn.SetActive(false);
            LightsOff.SetActive(true);
        }
    }
}
