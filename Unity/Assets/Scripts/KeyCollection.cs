using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public GameObject Key_Symbol_1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Key_Symbol_1.SetActive(true);
            FindObjectOfType<AudioManager>().Play("KeyCollectionSound");
            Destroy(collision.gameObject);
        }
    }
}
