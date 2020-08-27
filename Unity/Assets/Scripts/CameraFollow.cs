using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosistion = Player.transform.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosistion, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;        
    }
}
