using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPush : MonoBehaviour
{
    public float InteractionDistance = 1f;  // Distance to cast the ray for box interaction
    public LayerMask boxMask;               // layer mask for controlling layers of interaction 

    private GameObject box;                 // gameobject to store the box our ray hits 

    // Update is called once per frame
    void Update()
    {
        // set it so that ray we cast wont hit colliders it starts in
        Physics2D.queriesStartInColliders = false;
        // cast the ray called hit from this pos in the right direction which will swap if our character turns left , for the distance above and interacting with the layers in boxMask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,InteractionDistance,boxMask);
        // if the ray hits an object with the collider puzzlepiece and the player is holding e

        if (hit.collider != null && hit.collider.gameObject.layer == 9 && Input.GetKeyDown(KeyCode.E))
        {   // set the box to be equal to the object we hit 
            box = hit.collider.gameObject;
            // access the object and set it so that the fixed point 2d is joined to the players body and thus the player can drag it 
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            FindObjectOfType<AudioManager>().Play("Box");
            // set the being pulled variable to true
            box.GetComponent<BoxPull>().beingPushed = true; // this line seemed to prevent stuff beneath it from running Ben, so I moved the audio call above it.
            
        }
        else if (Input.GetKeyUp(KeyCode.E) && box != null) // if the player releases the e key and the box is not equal to null
        {
            // disable the fixed joint 2d so the box is no longer stuck to the player
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;            
        }
    }

    // used for testing 
    private void OnDrawGizmos()
    {
        // draw the ray in blue
        Gizmos.color = Color.blue;
        // draw the ray
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x *InteractionDistance);
    }
}
