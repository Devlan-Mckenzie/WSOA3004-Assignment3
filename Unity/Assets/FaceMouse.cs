using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    private Vector3 target;
    public GameObject Torch;

    private void Start()
    {
        //Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        target = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 differnce = target - Torch.transform.position;
        float rotationZ = Mathf.Atan2(differnce.y, differnce.x) * Mathf.Rad2Deg;
        Torch.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
        
    }

    
}
