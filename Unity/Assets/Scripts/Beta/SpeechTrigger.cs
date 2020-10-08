using System.Collections;
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
    public string[] EnemyWords;
    private float EnemyHealth;

    public Text PlayerSpeech;
    public string[] PlayerWords;
    private float PlayerHealth;

    private float Timer;

    public bool EnemyWinning;
    public bool PlayerWinning;

    private bool show=true;
    


    public int k;


    // Start is called before the first frame update
    void Start()
    {
        //EnemyWords= "Some quippy shit";
        //PlayerWords = "even more quippy shit!!!!!!!";        
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

        //k = SceneManager.GetActiveScene().buildIndex;
        

        EnemySpeech.text = EnemyWords[k];
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
        PlayerSpeech.text = PlayerWords[k];
    }
    // Update is called once per frame
    void Update()
    {
        

        Enemy[] EnemyHealth = FindObjectsOfType<Enemy>();
        foreach (Enemy H in EnemyHealth)
        {

           
            PlayerHealth = FindObjectOfType<PlayerCombat>().currentHealth;

            if (H.currentHealth < FindObjectOfType<PlayerCombat>().currentHealth)
            {
                PlayerWinning = true;
                EnemyWinning = false;

            }

            else if (H.currentHealth > FindObjectOfType<PlayerCombat>().currentHealth)
            {
                PlayerWinning = false;
                EnemyWinning = true;
            }

            if (H.currentHealth <= 60 && PlayerWinning && show)
            {
                GeneralSpeechUI.SetActive(true);
                Timer += Time.deltaTime;
                EnemyBanter();
                Invoke("PlayerBanter", 3);
                show = false;


            }

            else if (PlayerHealth <= 60 && EnemyWinning && show)
            {
                Timer += Time.deltaTime;
                PlayerBanter();
                Invoke("EnemyBanter", 3);
                GeneralSpeechUI.SetActive(true);
                show = false;
                
            }
        }

        if (Timer > 6)
        {
            PlayerSpeechBubble.SetActive(false);
            EnemySpeechBubble.SetActive(false);
            GeneralSpeechUI.SetActive(false);

        }
    }
}
