using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    private Vector3 target;     // store the vector3 we are aiming at
    public GameObject Torch;    // Store the torch of the player

    private void Start()
    {
        // can be uncommneted to make the cursor invisible
        //Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        // set the target equal to the world point if the mouse x and y and the cameras z position, as we are in 2d the z shouldnt make a difference
        target = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        //store the difference between the target and the torch transform, essentially the direct from the torch to the target
        Vector3 differnce = target - Torch.transform.position;
        // work out and store the rotation of the z axis required to face the above directions
        float rotationZ = Mathf.Atan2(differnce.y, differnce.x) * Mathf.Rad2Deg;
        // set the rotation to the above worked out rotation, notice there is a 90 degree offset. The reason for this was unknown however this + 90 degrees has been tested and now produces the correct rotation.
        Torch.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
        
    }

    
}
