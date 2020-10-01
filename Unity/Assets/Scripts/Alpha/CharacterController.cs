using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_maxSpeed = 10f;                     // The fastest the player can travel in the X - axis.
    [SerializeField] private float m_jumpForce = 400f;                   // Amount of force added when the player jumps.
    [SerializeField] private bool m_airControl = false;                  // Whether or not the player can steer while jumping.
    [SerializeField] private LayerMask m_whatIsGround = 1;                   // A mask determining what is ground to the character.
    [SerializeField] [Range(0, 1)] private float m_crouchSpeed = 0.3f;   // Percent of maxSpeed applied to the crouch.
    [SerializeField] private float m_climbSpeed = 10f;                   // Climbing speed at which the player will ascend

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
    private bool m_climbingUp = false;                                   // For Controlling ladder anim
    private bool m_climbingDown = false;                                 // For Controlling ladder anim
    private bool canClimb = false;                                       // Controls the ability to climb

    public GameObject Torch;
    public GameObject DeathPanel;                                                  // refrence to light in players hand 

    private float DeathAnimTimer = 0f;                                  // Timer for counting till game freeze after player death
    public float DeathAnimLength = 1f;                                  // Time for death animation to complete
    private bool StartDeathTimer = false;                               // bool that starts the timer for the death animation
    
    
    private void Awake()
    {
        // Setting up references.
        Time.timeScale = 1;
        m_groundCheck = transform.Find("GroundCheck");
        m_ceilingCheck = transform.Find("CeilingCheck");        
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();

        DeathPanel.SetActive(false);
    }      
    private void Update()
    {
        //if (!m_jump)
        //{
        //    // Read the jump input in Update so button presses aren't missed.
        //    m_jump = Input.GetButtonDown("Jump");
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_jump = true;
        }

        // read these inputs for up and down w s 
            if (Input.GetKeyDown(KeyCode.W))
            {
                m_climbingUp = true;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                m_climbingDown = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                m_climbingUp = false;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                m_climbingDown = false;
            }


        // Read the crouch input in update so button presses aren`t missed.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            m_crouch = true;
        }
        else
        {
            m_crouch = false;           
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleTorch();
        }

        if (StartDeathTimer)
        {
            DeathAnimTimer += Time.deltaTime;

            if (DeathAnimTimer > DeathAnimLength)
            {
                Time.timeScale = 0;
                DeathPanel.SetActive(true);
            }
        }
    } 
    void ToggleTorch()
    {
        Torch.SetActive(!Torch.activeSelf);
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
        if (!StartDeathTimer)
        {
            Move(h, m_crouch, m_jump, m_climbingUp, m_climbingDown);           
        }
        
        m_jump = false;        
    }   

    public void Setclimb(bool Climb)
    {
        canClimb = Climb;
    }
    public void Move(float move, bool crouch, bool jump,bool climbingUp , bool climbingDown)
    {   // If crouching then check to see if the player can stand up 
        if (!crouch && m_anim.GetBool("Crouch"))
        {
            // If the character has an object above them preventing them from standing then keep them crouching
            if (Physics2D.OverlapCircle(m_ceilingCheck.position,k_ceilingRadius,m_whatIsGround))
            {
                //crouch = true;      Disabled due to lack of use and error in this code          
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
                FindObjectOfType<AudioManager>().Play("BreathingSfx");
                FindObjectOfType<AudioManager>().Play("Run");
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_facingRight)
            {
                // ... flip the player.
                Flip();
                FindObjectOfType<AudioManager>().Play("BreathingSfx");
                FindObjectOfType<AudioManager>().Play("Run");
            }
        }
        // If the player should jump...
        if (m_grounded && jump)// && m_anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_grounded = false;
            m_anim.SetBool("Ground", false);
            m_rigidbody2D.AddForce(new Vector2(0f, m_jumpForce));

            FindObjectOfType<AudioManager>().Play("Jump");
        }

        if (m_climbingUp && canClimb)
        {            
            m_anim.SetBool("Climbing", true);
            m_rigidbody2D.position = new Vector2(m_rigidbody2D.position.x,m_rigidbody2D.position.y + m_climbSpeed);
        }

        if (m_climbingDown && canClimb)
        {
            m_anim.SetBool("Climbing", true);
            // removed speed as it would go through the floor
        }

        if (!canClimb)
        {
            m_anim.SetBool("Climbing", false);
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
    public void PlayerDeath()
    {
        m_anim.SetBool("Death", true);
        StartDeathTimer = true;

        FindObjectOfType<AudioManager>().Play("Death");
    }
    public bool PlayerisDead() { return StartDeathTimer; }
}

