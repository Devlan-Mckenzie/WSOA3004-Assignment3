using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AlarmLightToggle : MonoBehaviour
{
    public GameObject[] AlarmLights;

    public void ToggleGreen()
    {
        Debug.Log("Green");
        for (int i = 0; i < AlarmLights.Length; i++)
        {
            AlarmLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = Color.green;
        }
    }

    public void ToggleRed()
    {
        Debug.Log("Red");
        for (int i = 0; i < AlarmLights.Length; i++)
        {
            AlarmLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = Color.red;
        }
    }
}
