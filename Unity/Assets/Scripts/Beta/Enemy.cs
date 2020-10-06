using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator; 
    public int maxHealth = 100;
    public int currentHealth;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetBool("isDead", true);
        Player.GetComponent<PlayerCombat>().PlayerWon();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        this.enabled = false;
    }


    public void EnemyWon()
    {
        animator.SetTrigger("Won");
    }
}
