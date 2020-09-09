using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public List<GameObject> Key_Symbol; // list of keys 
    public List<GameObject> FinalKey_Symbol; // list of final keys 
   

    private int keyCount = 0;       // set the initial values to 0 and false
    public int FinalKeyCount = 0;
    public bool pickedupkey = false;

    public float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        // check if the player has atleast 1 key
        if (keyCount > 0)
        {
            // set the pickedupkey to true
            pickedupkey = true;
        } else
        {
            // if the player has 0 keys set the pickedupkey to false
            pickedupkey = false;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player collides an object tagged as friend
        if (collision.gameObject.tag == "Friend")
        {
            if (timer >= 5)
            {
                // increase the final key count by 1
                FinalKeyCount++;
                // find the audio manager and play the key collection sound
                FindObjectOfType<AudioManager>().Play("Key");
                //destroys the friend
                //(collision.gameObject);
                
                
                collision.gameObject.GetComponent<FriendMovement>().BeginMoveFriend = true;
                // loops from 0 to number of final keys

                for (int i = 0; i < FinalKey_Symbol.Count; i++)
                {
                    // sets the final key symbol to true if its less than the number of final keys obtained
                    FinalKey_Symbol[i].SetActive(i < FinalKeyCount);
                    timer = 0;
                }
            }
            
      
        }

        if (collision.gameObject.tag == "Key")
        {
            // increase the key count by 1
            keyCount++;
            // Find the audio manager and play the key collection sound
            FindObjectOfType<AudioManager>().Play("Key");
            // destroy the key
            Destroy(collision.gameObject);            
            // loop from 0 to the number of key items in the list
            for (int i = 0; i < Key_Symbol.Count; i++)
            {
                // sets the key symbol to true if its less than the number of keys obtained
                FinalKey_Symbol[i].SetActive(i < FinalKeyCount);
            }
        }
    }
    public void RemoveKey()
    {
        // Decrease the key count by 1 
        keyCount--;
        // if the key count is above or equal to 1
        if (keyCount >= 1)
        {
            // access the list and set the keycount - 1 key to inactive
            Key_Symbol[keyCount - 1].SetActive(false);
        }            
        else if(keyCount == 0) // if the key count is equal to zero
        {
            // set the 0th key to inactive
            Key_Symbol[keyCount].SetActive(false);
        }
        
    }

}
