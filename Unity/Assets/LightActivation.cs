using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject Torch;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float rayLength = 100f;

    private Vector2 HitVector;
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
                Debug.Log("LightTrigger Hit");
            }

            Debug.DrawLine(Torch.transform.position, hit.point, Color.red);
            HitVector = hit.point;
        }

    }

    


}
