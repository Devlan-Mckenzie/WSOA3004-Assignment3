using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject[] Doors;
    public GameObject[] Ladders;
    public bool testbool;

    public void OpenDoor()  
    {
        
        this.gameObject.SetActive(false);
        
        if(Doors.Length >= 1)
        {
            int n = 0;
            while (n< Doors.Length)
            {
                Doors[n].SetActive(false);
                n++;
            }
            
        }

        if(Ladders.Length >0)
        {
            int n = 0;
            while (n< Ladders.Length)
            {
                Ladders[n].SetActive(true);
                n++;
            }
        }
    }

    

    public void Update()
    {
        if (testbool)
        {
            OpenDoor();
        }
    }
}
