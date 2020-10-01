using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawnExists : MonoBehaviour
{   
    // Start is called before the first frame update
    private void Start()
    {
        if (this.gameObject.scene.buildIndex == -1)
        {
            // Do nothing
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
