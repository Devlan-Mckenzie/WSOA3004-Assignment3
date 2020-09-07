using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public GameObject[] Key_Symbol;
    

    public GameObject FinalKey_Symbol_1;
    public GameObject FinalKey_Symbol_2;
    public GameObject FinalKey_Symbol_3;

    private int keyCount = 0;
    public int FinalKeyCount = 0;
    public bool pickedupkey = false;

    private void Update()
    {
        if (keyCount > 0)
        {
            pickedupkey = true;
        } else
        {
            pickedupkey = false;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Friend")
        {
            FinalKeyCount++;
            FindObjectOfType<AudioManager>().Play("KeyCollectionSound");
            Destroy(collision.gameObject);
            if (FinalKeyCount >= 1)
            {
                FinalKey_Symbol_1.SetActive(true);
            }else
            {
                FinalKey_Symbol_1.SetActive(false);
            }

            if (FinalKeyCount >= 2)
            {
                FinalKey_Symbol_2.SetActive(true);
            }
            else
            {
                FinalKey_Symbol_2.SetActive(false);
            }

            if (FinalKeyCount >= 3)
            {
                FinalKey_Symbol_3.SetActive(true);
            }
            else
            {
                FinalKey_Symbol_3.SetActive(false);
            }
        }

        if (collision.gameObject.tag == "Key")
        {
            keyCount++;
            FindObjectOfType<AudioManager>().Play("KeyCollectionSound");
            Destroy(collision.gameObject);
            if (keyCount >= 1)
            {
                Key_Symbol[keyCount - 1].SetActive(true);
            }
        }
    }
    public void RemoveKey()
    {
        keyCount--;
        if (keyCount >= 1)
        {
            Key_Symbol[keyCount - 1].SetActive(false);
        }
            
        else if(keyCount == 0)
        {
            Key_Symbol[keyCount].SetActive(false);
        }
        
    }

}
