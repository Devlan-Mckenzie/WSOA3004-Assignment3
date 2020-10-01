using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    private new Rigidbody2D rigidbody2D;

    private Animator animator;
    private GameObject Player;
    private bool facingRight = false;    
   
    private float minDistance = 1f;
    private float range;
    private bool isChasing = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        range = Vector2.Distance(transform.position, Player.transform.position);

        if (range > minDistance)
        {
            Debug.Log(range);
            isChasing = true;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, maxSpeed * Time.deltaTime);
        }
        else
        {
            isChasing = false;
        }

        if (rigidbody2D.velocity.x > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (rigidbody2D.velocity.x < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

    }

    private void FixedUpdate()
    {
        animator.SetBool("Chasing", isChasing);
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
