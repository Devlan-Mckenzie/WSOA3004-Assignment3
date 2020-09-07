using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public int finalKeycount = 0;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(finalKeycount == 3 && collision.gameObject.tag== "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        finalKeycount = FindObjectOfType<KeyCollection>().FinalKeyCount; 
    }

}
