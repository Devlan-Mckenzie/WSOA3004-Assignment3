using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTimer : MonoBehaviour
{
    public float TimeGiven = 10;

    private float TimerCount = 0;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] bool StartTimer = false;
    [SerializeField] bool EnemySpawned = false;
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
            TimerCount += Time.deltaTime;         
        }

        if (TimerCount > TimeGiven && StartTimer)
        {
            StartTimer = false;            
            Instantiate(EnemyPrefab);
            EnemySpawned = true;           
        }
    }
}
