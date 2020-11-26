using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public bool isPaused = false;
    public Canvas PauseMenu;

    //added this function because a death wasnt coded
    void Start()
    {
        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                PauseMenu.enabled = false;
            }

            else
            {
                Time.timeScale = 0;
                isPaused = true;
                PauseMenu.enabled = true;
            }

            
            
        }

        
    }
}
