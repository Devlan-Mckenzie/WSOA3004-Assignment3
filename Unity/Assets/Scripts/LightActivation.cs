﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject Torch;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float rayLength = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Torch == null)
        {
            Torch = GameObject.FindGameObjectWithTag("Torch");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
    }

    // Update is called once per frame
    void Update()
    {   //Check if torch is on
        if (Torch.activeSelf)
        {   //get mouse position in world space
            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));

            //get direction vector from torch to mouse position in world space
            Vector3 direction = Torch.transform.position - worldMousePosition;

            RaycastHit2D hit = Physics2D.Raycast(Torch.transform.position, direction,rayLength);

            if (hit.collider != null && hit.collider.CompareTag("LightTrigger"))
            {
                hit.collider.GetComponent<LightTrigger>().OpenDoor();
                Debug.Log("Hit Light Trigger");
            }

            if (hit.collider != null && hit.collider.CompareTag("Redirect"))
            {
                hit.collider.GetComponent<LightRedirect>().LightOn();
                Debug.Log("Hit Redirect");
            }

            if (hit.collider != null && hit.collider.CompareTag("Semi"))
            {
                hit.collider.GetComponent<LightInteractable>().ToggleInteractable();
                Debug.Log("Hit Interactable");
            }


            //Debug.DrawLine(Torch.transform.position, hit.point, Color.red);            
        }
    }
}