using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomTriggers : MonoBehaviour
{
    //this is the ui to let player know that they can press the E to use a switch
    public GameObject PressToUseButton;
    private Rigidbody2D Player;

    //this should be assigned to the 3 different types of lighting that will be included in the first room

    //for little to no light
    public GameObject MinimalLighting;
    //for when the generator turns on, i made it a red light
    public GameObject GeneratorLighting;
    //for when the lights come back
    public GameObject GlobalLighting;

    public GameObject AlarmLight;

    public static int RoomCounter;

    public bool died = false;
    public bool Alarm = false;
    public bool CablePossession = false;

    public AudioSource AlarmSound;

    public Animator ClosingLockers;
    public Animator PlacingCable;
    public Animator SwitchingOnGenerator;

    public static float health;

    public GameObject Armoury1;
    public GameObject Armoury2;
    public Sprite LockedArmoury;

    public GameObject poweroff;
    public GameObject poweron;
    public AudioSource Switchtoggle;
    public AudioSource LockWeaponsSound;

    public GameObject generatorunplugged;
    public GameObject generatorplugged;

    public float SceneChangeDelay = 3;
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

        if (FindObjectOfType<Enemy>().currentHealth <= 0)
        {
            if (collision.gameObject.tag == "Room trigger")
            {
                PressToUseButton.SetActive(true);
            }
        }            
    }

    private void OnTriggerStay2D(Collider2D Roomtrigger)
    {
        if(FindObjectOfType<Enemy>().currentHealth <= 0)
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
            }

            if (Roomtrigger.gameObject.tag == "WeaponsLocker" && Input.GetKeyDown(KeyCode.E))
            {
                //plays animation to lock the weapons away 
                LockWeapons();
            }

            if (Roomtrigger.gameObject.tag == "Alarm Trigger" && Input.GetKeyDown(KeyCode.E))
            {
                //plays the alarm sound
                SoundAlarm();
            }

            if (Roomtrigger.gameObject.tag == "Final Trigger" && Input.GetKeyDown(KeyCode.E))
            {
                CablePlacement();
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D Roomtrigger)
    {
        if (Roomtrigger.gameObject.tag == "Room trigger")
        {
            PressToUseButton.SetActive(false);            
        }
    }

    public void LightSwitch()
    {
        //will just switch on the generator lights
        
        MinimalLighting.SetActive(false);
        GeneratorLighting.SetActive(true);
        //must insert animation here

        //Switchtoggle.Play(); MUST BE USED WHEN SOUND IS FOUND FOR NOW CAUSES ISSUES

        RoomCounter += 1;
        //SwitchingOnGenerator.Switch = true;
        generatorplugged.SetActive(true);
        generatorunplugged.SetActive(false);
        Invoke("exitroom", SceneChangeDelay);
    }

    public void LockWeapons()
    {
        //play animation 
        RoomCounter += 1;
        Debug.Log("Locked weapons away");
        //put ui in to indicate that this has been done
        //just a simple text plus a sound ?

        ///
        //LockWeaponsSound.Play(); Use this when sound back 
        ///

        //ClosingLockers.Switch = true;
        if (OpenArmory.activeSelf)
        {
            OpenArmory.SetActive(false);
            ClosedArmory.SetActive(true);
        }
        Invoke("exitroom", SceneChangeDelay);
    }

    public void SoundAlarm()
    {
        //play sound for alarm
        RoomCounter += 1;        
        Alarm = true;
        AlarmSound.Play();
        //FindObjectOfType<AudioManager>().Play("Alarm");
        Invoke("exitroom", SceneChangeDelay);
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
            Invoke("exitroom", SceneChangeDelay);

            if (poweroff.activeSelf)
            {
                poweroff.SetActive(false);
                poweron.SetActive(true); 
            }
        }
        //PlacingCable.Switch = true;
    }

    public void exitroom()
    {
        //change the scene (go to next room)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Play Exit Room Audio
        LockWeaponsSound.Play();
    }

    public void deathscenechange()
    {
        SceneManager.LoadScene(7);
    }

    // Update is called once per frame
    void Update()
    {

        if (Alarm)
        {
            float timer = 1f;
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                if (AlarmLight.activeSelf)
                {
                    AlarmLight.SetActive(false);
                    timer = 1f;
                }
                else if(!AlarmLight.activeSelf)
                {
                    AlarmLight.SetActive(true);
                    timer = 1f;
                }
            }
        }
        if (FindObjectOfType<PlayerCombat>().currentHealth <= 0)
        {
            RoomCounter = 0;
            Invoke("deathscenechange", SceneChangeDelay);
        }        
    }
}
