using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanarDistance : MonoBehaviour
{
    private bool PlanarSet = false;
    public float NewPlanarDistance = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Canvas>().worldCamera != null && !PlanarSet)
        {
            this.GetComponent<Canvas>().planeDistance = NewPlanarDistance;
            PlanarSet = true;
        }
    }
}
