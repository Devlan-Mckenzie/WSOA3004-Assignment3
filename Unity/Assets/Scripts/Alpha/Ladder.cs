using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player enters the ladder trigger
        if (collision.gameObject.tag == "Player")
        {
            // set the players character controller to climb
            collision.gameObject.GetComponent<CharacterController>().Setclimb(true);
            FindObjectOfType<AudioManager>().Play("LadderFootsteps");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        // if the player stays inside the ladder trigger
        if (collision.gameObject.tag == "Player")
        {
            // set the player climb to true
            collision.gameObject.GetComponent<CharacterController>().Setclimb(true);
            FindObjectOfType<AudioManager>().Play("LadderFootsteps");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if the player leaves the ladder trigger
        if (collision.gameObject.tag == "Player")
        {
            // set the player climb to false
            collision.gameObject.GetComponent<CharacterController>().Setclimb(false);
        }

    }
}
