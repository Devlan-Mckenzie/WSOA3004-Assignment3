﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechTrigger : MonoBehaviour
{
    public GameObject GeneralSpeechUI;
    public GameObject EnemySpeechBubble;
    public GameObject PlayerSpeechBubble;

    //public Text speech;

    public Text EnemySpeech;
    private string EnemyWords;
    private float EnemyHealth;

    public Text PlayerSpeech;
    private string PlayerWords;
    private float PlayerHealth;

    private float Timer;

    public bool EnemyWinning;
    public bool PlayerWinning;

    // Start is called before the first frame update
    void Start()
    {
        EnemyWords= "Some quippy shit";
        PlayerWords = "even more quippy shit!!!!!!!";
        
    }

    public void EnemyBanter()
    {
        if (!EnemySpeechBubble.activeSelf && !PlayerSpeechBubble.activeSelf)
        {
            EnemySpeechBubble.SetActive(true);
        }

        else if (!EnemySpeechBubble.activeSelf && PlayerSpeechBubble.activeSelf)
        {
            PlayerSpeechBubble.SetActive(false);
            EnemySpeechBubble.SetActive(true);
        }

        else if(EnemySpeechBubble.activeSelf && PlayerSpeechBubble.activeSelf)
        {
            PlayerSpeechBubble.SetActive(false);
        }

        EnemySpeech.text = EnemyWords;
    }

    public void PlayerBanter()
    {
        if (!EnemySpeechBubble.activeSelf && !PlayerSpeechBubble.activeSelf)
        {
            PlayerSpeechBubble.SetActive(true);
        }

        else if (EnemySpeechBubble.activeSelf && !PlayerSpeechBubble.activeSelf)
        {
            PlayerSpeechBubble.SetActive(true);
            EnemySpeechBubble.SetActive(false);
        }

        else if (EnemySpeechBubble.activeSelf && PlayerSpeechBubble.activeSelf)
        {
            EnemySpeechBubble.SetActive(false);
        }
        PlayerSpeech.text = PlayerWords;
    }
    // Update is called once per frame
    void Update()
    {
        

        EnemyHealth = FindObjectOfType<Enemy>().currentHealth;
        PlayerHealth = FindObjectOfType<PlayerCombat>().currentHealth;

        if(EnemyHealth <= 60 && EnemyHealth >20)
        {
            GeneralSpeechUI.SetActive(true);
            Timer += Time.deltaTime;
            EnemyBanter();
            Invoke("PlayerBanter", 3);
            
            if (Timer > 6)
            {
                PlayerSpeechBubble.SetActive(false);
                GeneralSpeechUI.SetActive(false);
            }
        }

        else if (PlayerHealth <= 60 && PlayerHealth>20)
        {
            Timer += Time.deltaTime;
            PlayerBanter();
            Invoke("EnemyBanter", 3);
            GeneralSpeechUI.SetActive(true);
            if (Timer > 6)
            {
                EnemySpeechBubble.SetActive(false);
                GeneralSpeechUI.SetActive(false);
            }
        }
    }
}
