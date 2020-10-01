using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject Door2;

    private void Start()
    {
        Door = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(Door);
            Destroy(Door2);
            FindObjectOfType<KeyCollection>().RemoveKey();
            //FindObjectOfType<AudioManager>().Play("DoorOpen");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
