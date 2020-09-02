using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBox : MonoBehaviour
{
    private Rigidbody2D bodyr;

    private void Update()
    {
        if(bodyr.velocity.magnitude > 0)
        {
            FindObjectOfType<AudioManager>().Play("Noise");
        }
    }
}
