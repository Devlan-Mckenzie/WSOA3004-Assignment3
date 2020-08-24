using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_maxSpeed = 10f;                     // The fastest the player can travel in the X - axis.
    [SerializeField] private float m_jumpForce = 400f;                   // Amount of force added when the player jumps.
    [SerializeField] private bool m_airControl = false;                  // Whether or not the player can steer while jumping.
    [SerializeField] private LayerMask m_whatIsGround;                   // A mask determining what is ground to the character.
    [SerializeField] [Range(0, 1)] private float m_crouchSpeed = 0.3f;   // Percent of maxSpeed applied to the crouch.

    private Transform m_groundCheck;                                     // A position marking where to check if the player is grounded.
    const float k_groundedRadius = .2f;                                  // Radius of the overlap circle to determine if grounded.
    private bool m_grounded = false;                                     // For checking if grounded.
    private Transform m_ceilingCheck;                                    // A position marking where to check for ceilings
    const float k_ceilingRadius = .01f;                                  // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_rigidbody2D;                                   // A reference to the players rigidbody.
    private bool m_facingRight = true;                                   // For determining which way the player is currently facing.
    private bool m_jump = false;                                         // For controlling player jumps.
    private Animator m_anim;                                             // Reference to the player`s animator component. 
    private bool m_crouch = false;                                       // For controlling player crouch

    

    private void Awake()
    {
        // Setting up references.
        m_groundCheck = transform.Find("GroundCheck");
        m_ceilingCheck = transform.Find("CeilingCheck");        
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!m_jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_jump = Input.GetButtonDown("Jump");
        }

        // Read the crouch input in update so button presses aren`t missed.
        m_crouch = Input.GetKey(KeyCode.LeftControl);
        
    }
        

    private void FixedUpdate()
    {
        m_grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground        
       // Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_groundedRadius, m_whatIsGround);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_groundedRadius, m_whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_grounded = true;
        }
        m_anim.SetBool("Ground", m_grounded); // Sets the Animator Ground paramater of m_anim to the value of m_grounded  

        // Set the vertical animaiton speed 
        m_anim.SetFloat("vSpeed", m_rigidbody2D.velocity.y);


        // Read the inputs.        
        float h = Input.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        Move(h,m_crouch, m_jump);
        m_jump = false;        
    }   

    public void Move(float move, bool crouch, bool jump)
    {   // If crouching then check to see if the player can stand up 
        if (!crouch && m_anim.GetBool("Crouch"))
        {
            // If the character has an object above them preventing them from standing then keep them crouching
            if (Physics2D.OverlapCircle(m_ceilingCheck.position,k_ceilingRadius,m_whatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the player is crouching in the animator
        m_anim.SetBool("Crouch", crouch);
        
        //only control the player if grounded or airControl is turned on
        if (m_grounded || m_airControl)
        {
            // Reduce the speed if crouching my crouching multiplier
            move = (crouch ? move * m_crouchSpeed : move); // Short hand if then 

            // Sets the speed animator comp to absolute value of horizontal input
            m_anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_rigidbody2D.velocity = new Vector2(move * m_maxSpeed, m_rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_grounded && jump)// && m_anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_grounded = false;
            m_anim.SetBool("Ground", false);
            m_rigidbody2D.AddForce(new Vector2(0f, m_jumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

