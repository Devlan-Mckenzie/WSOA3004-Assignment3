using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBeta : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    private new Rigidbody2D rigidbody2D;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        // Read all the inputs 
        float v = Input.GetAxis("Horizontal");
        float h = Input.GetAxis("Vertical");
        Move(h, v);
    }

    void Move(float HorizontalMove, float VerticalMove)
    {
        // Move the character in the horizontal axis
        rigidbody2D.velocity = new Vector2(HorizontalMove * maxSpeed, rigidbody2D.velocity.y);

        //Move the Character in the Vertical Axis
        rigidbody2D.velocity = new Vector2(VerticalMove * maxSpeed, rigidbody2D.velocity.x);
    }
}
