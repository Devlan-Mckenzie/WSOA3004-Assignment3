using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBeta : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    private new Rigidbody2D rigidbody2D;

    private Animator animator;
    private bool facingRight = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Read all the inputs 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");        
        Move(h, v);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Vspeed", Mathf.Abs(rigidbody2D.velocity.y));
        animator.SetFloat("Hspeed", Mathf.Abs(rigidbody2D.velocity.x));
    }

    void Move(float HorizontalMove, float VerticalMove)
    {
        // Move the character in the horizontal axis
        rigidbody2D.velocity = new Vector2(HorizontalMove * maxSpeed, rigidbody2D.velocity.y);

        //Move the Character in the Vertical Axis
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, VerticalMove * maxSpeed);

        // If the input is moving the player right and the player is facing left...
        if (HorizontalMove > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();           
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (HorizontalMove < 0 && facingRight)
        {
            // ... flip the player.
            Flip();           
        }
    }

    private void Flip()
    {        
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
