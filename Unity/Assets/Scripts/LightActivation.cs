using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject Torch;   // stores the players light

    [SerializeField]
    private Camera mainCamera;      // reference to the main camera

    [SerializeField]
    private float rayLength = 100f;     // distance to cast the players ray cast
    
    // Start is called before the first frame update
    void Start()
    {
        // if the torch has not been assigned 
        if (Torch == null)
        {
            // assign the torch by tag Torch
            Torch = GameObject.FindGameObjectWithTag("Torch");
        }
        // if the main camera has not been assigned 
        if (mainCamera == null)
        {
            // assign it via the main camera method
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
            // cast a ray called hit in the direction above for the distance raylength
            RaycastHit2D hit = Physics2D.Raycast(Torch.transform.position, direction,rayLength);
            // if the ray hits an object with the light trigger tag
            if (hit.collider != null && hit.collider.CompareTag("LightTrigger"))
            {
                // access the object and play the opendoor function
                hit.collider.GetComponent<LightTrigger>().OpenDoor();

                FindObjectOfType<AudioManager>().Play("DoorOpen");
               
            }
            // if the ray hits an object with the redirect tag
            if (hit.collider != null && hit.collider.CompareTag("Redirect"))
            {
                // access the object and play the lighton function
                hit.collider.GetComponent<LightRedirect>().LightOn();
                FindObjectOfType<AudioManager>().Play("DoorOpen");
            }
            // if the ray hits and object with the tag semi
            if (hit.collider != null && hit.collider.CompareTag("Semi"))
            {
                // access the object and play the toggleinteractable function
                hit.collider.GetComponent<LightInteractable>().ToggleInteractable();
                FindObjectOfType<AudioManager>().Play("DoorOpen");
            }
            // Draw the ray for visual purposes
            //Debug.DrawLine(Torch.transform.position, hit.point, Color.red);            
        }
    }
}
