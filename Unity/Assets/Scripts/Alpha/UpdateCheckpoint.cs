using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateCheckpoint : MonoBehaviour
{   
    [Header("Variable for storing Respawn point")]
    [SerializeField] private GameObject CurrentCheckPoint;

    private GameObject Player;
    // Start is called before the first frame update
    private void Awake()
    {
        CurrentCheckPoint = GameObject.FindGameObjectWithTag("Spawn");
        DontDestroyOnLoad(CurrentCheckPoint);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        Player.transform.position = CurrentCheckPoint.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            CurrentCheckPoint.transform.position = collision.gameObject.transform.position;
            FindObjectOfType<AudioManager>().Play("CheckPoint");
            Destroy(collision.gameObject);            
        }
    }

}
