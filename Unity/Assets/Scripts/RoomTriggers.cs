using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomTriggers : MonoBehaviour
{
    public GameObject PressToUseButton;
    public Rigidbody2D Player;

    public GameObject MinimalLighting;
    public GameObject GeneratorLighting;
    public GameObject GlobalLighting;

    public static int RoomCounter;

    public bool died = false;
    public bool Alarm = false;
    public bool CablePossession = false;

    public static float health;
    // Start is called before the first frame update
    void Start()
    {
        
        //set the press to use button false
        PressToUseButton.SetActive(false);
        died = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LightCable")
        {
            CablePossession = true;
        }
    }


    private void OnTriggerStay2D(Collider2D Roomtrigger)
    {
        if (Roomtrigger.gameObject.tag == "Room trigger")
        {
            
            PressToUseButton.SetActive(true);
            
        }

        if (Roomtrigger.gameObject.tag == "Switch trigger" && Input.GetKeyDown(KeyCode.E))
        {
            //if E is pressed while the player is within the switch trigger , it will switch on the lights and set the instruction UI to false
            LightSwitch();
            Debug.Log("Room Trigger hit");
            PressToUseButton.SetActive(false);

            if (CablePossession)
            {
                CablePlacement();
            }
        }

        if (Roomtrigger.gameObject.tag == "WeaponsLocker" && Input.GetKeyDown(KeyCode.E))
        {
            //plays animation to lock the weapons away 
            LockWeapons();

        }

        if(Roomtrigger.gameObject.tag == "AlarmSwitch" && Input.GetKeyDown(KeyCode.E))
        {
            //plays the alarm sound
            SoundAlarm();

        }

        


    }

    private void OnTriggerExit2D(Collider2D Roomtrigger)
    {
        if (Roomtrigger.gameObject.tag == "Room trigger")
        {
            PressToUseButton.SetActive(false);
            Invoke("exitgame", 1);
        }
    }

    public void LightSwitch()
    {
        //will just switch on the generator lights
        
        MinimalLighting.SetActive(false);
        GeneratorLighting.SetActive(true);
        //must insert animation here
        RoomCounter += 1;
        
        
    }

    public void LockWeapons()
    {
        //play animation 
        RoomCounter += 1;
        Debug.Log("Locked weapons away");
        //put ui in to indicate that this has been done
        //just a simple text plus a sound ?

    }

    public void SoundAlarm()
    {
        //play sound for alarm
        RoomCounter += 1;
        Alarm = true;
       
    }

    public void CablePlacement()
    {
        //play animation

        //Checks if generatorlight is still active and if it is, makes it inactive and then switches on the lights again
        if (GeneratorLighting.activeSelf)
        {
            GeneratorLighting.SetActive(false);
            GlobalLighting.SetActive(true);
            Debug.Log("Lights are back on");
        }

    }
    public void exitroom()
    {
        //change the scene (go to next room)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Alarm)
        {
            float timer = 0.5f;
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                if (GeneratorLighting.activeSelf)
                {
                    GeneratorLighting.SetActive(false);
                    timer = 0.5f;
                }
                else if(!GeneratorLighting.activeSelf)
                {
                    GeneratorLighting.SetActive(true);
                    timer = 0.5f;
                }
            }
        }
        if (health <= 0)
        {
            died = true;
        }

        if (died)
        {
            RoomCounter = 0;
        }

        if (Alarm)
        {
            //make the lights switch between white and red to increase game feel , will put it in once the level has been put together
        }
    }
}
