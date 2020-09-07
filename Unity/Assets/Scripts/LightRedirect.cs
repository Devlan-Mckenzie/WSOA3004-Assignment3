using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRedirect : MonoBehaviour
{
    public GameObject Light;

    [SerializeField]
    public float rayLength = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Light.activeSelf)      
        {           

            //get direction vector from light to upwards position in world space
            Vector3 direction = transform.up;            

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, rayLength);

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

            Debug.DrawLine(this.transform.position, hit.point, Color.red);
        }
    }

    public void LightOn()
    {
        Light.SetActive(true);
    }
}
