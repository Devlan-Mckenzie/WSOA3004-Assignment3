using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSpin : MonoBehaviour
{
    public float SpinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * SpinSpeed);
    }
}
