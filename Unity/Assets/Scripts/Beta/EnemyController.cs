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
   
    public float minDistance = 1f;
    private float range;
    private bool isChasing = false;
    public int agroRange = 5;

    public Transform attackPoint;
    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;




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
        if (range < agroRange)
        {
            if (range > minDistance)
            {                
                isChasing = true;
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, maxSpeed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                
            }
        }
        else
        {
            isChasing = false;
        }
        

        if ( Player.transform.position.x - rigidbody2D.transform.position.x > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (Player.transform.position.x - rigidbody2D.transform.position.x < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerCombat>().PlayerTakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
