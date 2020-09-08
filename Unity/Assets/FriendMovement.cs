using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMovement : MonoBehaviour
{
    private bool FacingRight = true;
    [SerializeField]
    private bool RunRight = true;
    public float MoveSpeed = 10f;
    private Rigidbody2D rb;
     private float MoveDirection = 1;
    public bool BeginMoveFriend = false;

    public float DespawnTime = 5f;
    private float TimePassed = 0f;
    private bool StartDespawnTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (RunRight)
        {
            FacingRight = true;
            MoveDirection = 1;
        }
        else
        {
            FacingRight = false;
            MoveDirection = -1;
            Flip();            
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (BeginMoveFriend)
        {
            if (!StartDespawnTimer)
            {
                StartDespawnTimer = true;
            }

            if (StartDespawnTimer)
            {
                TimePassed += Time.deltaTime;
                if (TimePassed > DespawnTime)
                {
                    this.gameObject.SetActive(false);
                }
            }
            MoveFriend();
        }        
    }

    void MoveFriend()
    {
        rb.velocity = new Vector2(MoveDirection * MoveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
