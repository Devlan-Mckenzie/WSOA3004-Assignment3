using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject Door;
    public void OpenDoor()
    {
        Door.SetActive(false);
    }
}
