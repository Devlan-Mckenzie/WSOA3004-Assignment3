using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRedirect : MonoBehaviour
{
    public GameObject Light;    // stores a light on the box

    [SerializeField]
    public float rayLength = 100f;  // sets the distance the box can interact with

    // Update is called once per frame
    void Update()
    {
        // if the light is active
        if (Light.activeSelf)      
        {
            //get direction vector from light to upwards position in world space
            Vector3 direction = transform.up;            
            // cast a ray called hit in the upwards direction
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, rayLength);
            // if the ray collided with something and that something has the tag light trigger
            if (hit.collider != null && hit.collider.CompareTag("LightTrigger"))
            {
                // access the object the ray collided with and run the open door function 
                hit.collider.GetComponent<LightTrigger>().OpenDoor();                
            }
            // if the ray hit something and the tag on the object it hit is the redirect tag
            if (hit.collider != null && hit.collider.CompareTag("Redirect"))
            {
                // access the object hit and play the lighton function
                hit.collider.GetComponent<LightRedirect>().LightOn();              
            }
            // if the ray hit something and the tag on the object hit is semi
            if (hit.collider != null && hit.collider.CompareTag("Semi"))
            {
                // access the object hit and play the toggleinteractable function
                hit.collider.GetComponent<LightInteractable>().ToggleInteractable();               
            }
            // draw the ray so that we can visualise the ray
            Debug.DrawLine(this.transform.position, hit.point, Color.red);
        }
    }

    public void LightOn()
    {
        // sets the light attached to the box to true
        Light.SetActive(true);
    }
}
