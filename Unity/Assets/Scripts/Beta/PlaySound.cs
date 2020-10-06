using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip footsteps;
    public AudioSource audioS;

    void Footsetps()
    {
        audioS.PlayOneShot(footsteps);
    }

}
