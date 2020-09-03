using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [Header("Door Reference")]
    public GameObject Door_1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PuzzlePiece")
        {
            Door_1.SetActive(false);
        }

        if(collision.gameObject.tag == "Player" && FindObjectOfType<KeyCollection>().pickedupkey )
        {
            Door_1.SetActive(false);
            FindObjectOfType<KeyCollection>().RemoveKey();
        }
    }
}
