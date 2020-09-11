﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public int finalKeycount = 0;    // Stores the final key count
    public int RequiredFinalKeys = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player collides with this door and has 3 final keys 
        if(finalKeycount == RequiredFinalKeys && collision.gameObject.tag== "Player")
        {
            // destroy this door
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // set the final key count to the final key count variable in the key collection script on the player
        finalKeycount = FindObjectOfType<KeyCollection>().FinalKeyCount; 
    }

}
