using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject PauseMenu;

    //added this function because a death wasnt coded
    public void deathscenechange()
    {
        SceneManager.LoadScene(7);
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
                PauseMenu.SetActive(false);
            }

            else
            {
                Time.timeScale = 0;
                isPaused = true;
                PauseMenu.SetActive(true);
            }

            
            
        }

        if (FindObjectOfType<PlayerCombat>().currentHealth <= 0)
        {
            Invoke("deathscenechange", 1.5f);
        }
    }
}
