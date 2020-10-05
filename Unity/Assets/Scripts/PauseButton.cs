﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }

            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
            
        }
    }
}
