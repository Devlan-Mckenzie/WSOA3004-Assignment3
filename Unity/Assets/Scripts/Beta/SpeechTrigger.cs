﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechTrigger : MonoBehaviour
{
    //public GameObject GeneralSpeechUI;
    [Header ("SpeechBubbles")]
    public GameObject EnemySpeechBubble;
    public GameObject PlayerSpeechBubble;

    [Header("Image Behind Characters During Speech")]
    public GameObject SpeedlinesImage;

    //public Text speech;
    [Header ("")]
    public Text EnemySpeech;
    public string[] EnemyWords;
    [Header("")]
    public Text PlayerSpeech;
    public string[] PlayerWords;

    private float EnemyHealth;
    private float PlayerHealth;

    [Header ("HP Threshold for comments")]
    public int HealthThreshold = 60;

    public bool EnemyWinning;
    public bool PlayerWinning;
    private bool hasPlayedHealthThreshold = false;
    private bool hasPlayedDeath = false;

    private int EnemyComment;
    private int PlayerComment;

    private bool startTimerPlayer = false;
    private bool startTimerEnemy = false;
    private float TimerPlayer;
    private float TimerEnemy;
    [Header("Time in seconds for comments to fade")]
    public float CommentFade = 6f;
    [Header("Time in seconds for reply from other person")]
    public float ReplyTime = 3f;

    public Animator cameraAnimation;
    public int PlayerZoomCount = 0;
    public int EnemyZoomCount = 0;




    // Start is called before the first frame update
    void Start()
    {
        //EnemyWords= "Some quippy shit";
        //PlayerWords = "even more quippy shit!!!!!!!";      
        SpeedlinesImage = GameObject.FindGameObjectWithTag("SpeedLines");
        PlayerSpeechBubble = GameObject.FindGameObjectWithTag("PlayerSpeechBubble");
        PlayerSpeech = PlayerSpeechBubble.GetComponentInChildren<Text>();
        PlayerBanter();
        Invoke("EnemyBanter", ReplyTime);
    }

    public void EnemyBanter()
    {
        //Update Speech bubble text 
        EnemySpeech.text = EnemyWords[EnemyComment];
        //Increase array refrence integer by 1
        EnemyComment++;
        //Display enemy speech bubble if called 
        EnemySpeechBubble.SetActive(true);
        
        //Start the enemy timer for comments to fade
        startTimerEnemy = true;
        EnemyZoomCount++;
        
        FindObjectOfType<CharacterControllerBeta>().ActiveCutScene = true;
        if (EnemyZoomCount !=2)
        {
            
            cameraAnimation.SetInteger("CameraSelect", 2);
            SpeedlinesImage.GetComponent<Image>().enabled = true;
        }

    }

    public void PlayerBanter()
    {
        //Update speech bubble text
        PlayerSpeech.text = PlayerWords[PlayerComment];
        //Increment refrence variable by 1 
        PlayerComment++;
        //Display Player speech bubble if called 
        PlayerSpeechBubble.SetActive(true);
        
        //start the player timer for comments to fade
        startTimerPlayer = true;
        PlayerZoomCount++;
        
        FindObjectOfType<CharacterControllerBeta>().ActiveCutScene = true;
        if (PlayerZoomCount!=2)
        {
            
            cameraAnimation.SetInteger("CameraSelect", 1);
            SpeedlinesImage.GetComponent<Image>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Enemy[] EnemyHealth = FindObjectsOfType<Enemy>();
        PlayerHealth = FindObjectOfType<PlayerCombat>().currentHealth;
        foreach (Enemy EnemyScript in EnemyHealth)
        {
            CheckWinning(EnemyScript);
            CheckHealthLevel(EnemyScript);
            
        }

        if (startTimerEnemy)
        {
            TimerEnemy += Time.deltaTime;
            if (TimerEnemy > CommentFade)
            {                
                EnemySpeechBubble.SetActive(false);
                SpeedlinesImage.GetComponent<Image>().enabled = false;
                startTimerEnemy = false;
                TimerEnemy = 0;
                cameraAnimation.SetInteger("CameraSelect", 0);
                FindObjectOfType<CharacterControllerBeta>().ActiveCutScene = false;

            }
        }

        if (startTimerPlayer)
        {
            TimerPlayer += Time.deltaTime;
            if (TimerPlayer > CommentFade)
            {
                PlayerSpeechBubble.SetActive(false);
                SpeedlinesImage.GetComponent<Image>().enabled = false;
                startTimerPlayer = false;
                TimerPlayer = 0;
                cameraAnimation.SetInteger("CameraSelect", 0);
                FindObjectOfType<CharacterControllerBeta>().ActiveCutScene = false;
            }
        }
    }

    void CheckWinning(Enemy EnemyScript)
    {
        if (EnemyScript.currentHealth <= PlayerHealth)
        {
            PlayerWinning = true;
            EnemyWinning = false;
        }
        else if (EnemyScript.currentHealth > PlayerHealth)
        {
            PlayerWinning = false;
            EnemyWinning = true;
        }
    }

    void CheckHealthLevel(Enemy EnemyScript)
    {
        if (EnemyScript.currentHealth <= HealthThreshold && PlayerWinning && !hasPlayedHealthThreshold)
        {
            //GeneralSpeechUI.SetActive(true);            
            EnemyBanter();
            Invoke("PlayerBanter", ReplyTime);
            hasPlayedHealthThreshold = true;
        }
        else if (PlayerHealth <= HealthThreshold && EnemyWinning && !hasPlayedHealthThreshold)
        {
            //GeneralSpeechUI.SetActive(true);
            PlayerBanter();
            Invoke("EnemyBanter", ReplyTime);
            hasPlayedHealthThreshold = true;
        }

        if (EnemyScript.currentHealth <= 0 && !hasPlayedDeath)
        {
            //GeneralSpeechUI.SetActive(true);
            PlayerBanter();
            hasPlayedDeath = true;
        }

        if (PlayerHealth <= 0 && !hasPlayedDeath)
        {
            //GeneralSpeechUI.SetActive(true);
            EnemyBanter();
            hasPlayedDeath = true;
        }
    }
}
