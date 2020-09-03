using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    public Slider TimerSlider;
    public GameObject TextStorage;
    public TextMeshProUGUI  TimerText;
    public float TimeGiven = 10;

    private float TimerCount = 0;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] bool StartTimer = false;
    [SerializeField] bool EnemySpawned = false;
    [SerializeField] bool PuzzleCompleted = false;
    float SliderTime;

    private void Start()
    {
        StartTimer = false;
        TimerSlider.maxValue = TimeGiven;
        TimerSlider.value = TimeGiven;

        TimerText = TextStorage.GetComponent<TextMeshProUGUI>();

        SliderTime = TimeGiven;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !EnemySpawned)
        {
            StartTimer = true;
        }
    }

    private void Update()
    {

        if (StartTimer)
        {
            TimerSlider.gameObject.SetActive(true);
            SliderTime -= Time.deltaTime;
            float SliderMinutes = Mathf.FloorToInt(SliderTime / 60);
            float SliderSeconds =  Mathf.FloorToInt(SliderTime - SliderMinutes *60f);    
            
            string textTime = string.Format("{0:0}:{1:00}", SliderMinutes, SliderSeconds);

            if (SliderTime <= 0)
            {
                StartTimer = false;
            }          
            
            TimerText.text = textTime;
            TimerSlider.value = SliderTime;            
        }
        

        if (!PuzzleCompleted)
        {
            if (TimerCount > TimeGiven && StartTimer)
            {
                StartTimer = false;
                Instantiate(EnemyPrefab);
                FindObjectOfType<AudioManager>().Play("EnemyGrowl");
                EnemySpawned = true;
            }
        }
        else
        {
            TimerSlider.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PuzzleComplete")
        {
            PuzzleCompleted = true;
        }
        
    }


}
