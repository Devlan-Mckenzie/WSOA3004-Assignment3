﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 40;
    public int attackStamina = 20;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Animator animator;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public int maxStamina = 100;
    public int currentStamina;

    public StaminaBar staminaBar;

    public int healthRegen = 1;
    public int staminaRegen = 1;
    private float nextHealthRegenTime = 0f;
    private float nextStaminaRegenTime = 0f;
    public float healthRegenRate = 1f;
    public float staminaRegenRate = 1f;

    private Rigidbody2D PlayerRB2D;
    public bool isAlive = true;

    private GameObject Prisoner;
    public AudioSource hitSound;

    public ParticleSystem ThrustParticles;

    public GameObject ShadowPlayerAlive;
    public GameObject ShadowPlayerDead;

    public List<Transform> AttackChecks; // Stores the check pos in space 
    private bool HitEnemy = false; // disable the attack after its hit the enemy during that attack anim
    public float MultiAttackRange = 0.5f; // as we use new attack points we need new attack range as well
    public float KnockBackForce = 10f;//knock back force 
    private Vector2 moveDirection;

    private bool inPain = false;
    public float PainLength = 2f;
    private float PainTime = 0f;
    private bool canTakeDmg = true;

    private void Start()
    {
        PlayerRB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        Prisoner = GameObject.FindGameObjectWithTag("Enemy");
        HitEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<CharacterControllerBeta>().ActiveCutScene)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && currentStamina >= attackStamina && !inPain)
                {
                    Debug.Log("Mouse click");
                    Attack();
                    Debug.Log("Attack was Called before");
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            HealthRegen();
            StaminaRegen();

            if (inPain)
            {
                PainTime += Time.deltaTime;
                if (PainLength < PainTime)
                {
                    PainTime = 0f;
                    inPain = false;
                    canTakeDmg = true;
                }
            }
        }
        
    }

    void HealthRegen()
    {
        if (Time.time >= nextHealthRegenTime)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healthRegen;
                healthBar.SetHealth(currentHealth);
                nextHealthRegenTime = Time.time + 1f / healthRegenRate;
            }
        }
    }

    void StaminaRegen()
    {
        if (Time.time >= nextStaminaRegenTime)
        {
            if (currentStamina < maxHealth)
            {
                currentStamina += staminaRegen;
                staminaBar.SetStamina(currentStamina);
                nextStaminaRegenTime = Time.time + 1f / staminaRegenRate;
            }
        }
    }
    void Attack()
    {
        Debug.Log("Attacking call ");
        currentStamina -= attackStamina;
        staminaBar.SetStamina(currentStamina);

        //Play attack animation
        animator.SetTrigger("Attack");
        Debug.Log("Attack Trigger set");

        //Detect enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            moveDirection = enemy.transform.position - this.transform.position;   
            enemy.GetComponent<EnemyController>().SetinPain();
            enemy.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized * KnockBackForce);
            hitSound.Play();
        }
    }

    public void AttackMulti(int AttackPointNum)
    {
        if (!HitEnemy)
        {
            //Detect enemies hit
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackChecks[AttackPointNum].position, MultiAttackRange, enemyLayers);// Updated to use a point in a list of attack points, new range same affected layers 

            //Damage enemies
            foreach (Collider2D enemy in hitEnemies) // same check performed in smaller area multiple times per animation of attack 
            {
                HitEnemy = true; // if this plays one enemy was hit and the dmg dealt is set to true so u cant dmg again in the same swing 
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);//access the enemy and deal dmg 
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(KnockBackForce, 0));
                hitSound.Play();//play the hit sound 

                //create the hit particle affect here

            }
        }
        else
        {
            return;
        }        
    }

    public void ResetHitEnemy()//Use anim event to call this at the end of attack anime to reset player ability to attack an enenmy 
    {
        HitEnemy = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        for (int i = 0; i < AttackChecks.Count; i++)
        {
            Gizmos.DrawWireSphere(AttackChecks[i].position, MultiAttackRange);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerRB2D.velocity = Vector2.zero;
        isAlive = false;
        animator.SetBool("isDead", true);
        HideBars();
        SwapShadows();
        Prisoner.GetComponent<Enemy>().EnemyWon();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<CharacterControllerBeta>().enabled = false;
        this.enabled = false;
    }

    public void PlayerWon()
    {
        animator.SetTrigger("Won");
    }

    void HideBars()
    {
        healthBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
    }

    void SwapShadows()
    {
        ShadowPlayerAlive.SetActive(false);
        ShadowPlayerDead.SetActive(true);
    }
    void CreateThrustParticles()
    {
        ThrustParticles.Play();
    }

    public void SetInPain()
    {
        inPain = true;
    }

    public bool CanTakeDamage()
    {
        return canTakeDmg;
    }

    public void DisableDamageTaken()
    {
        canTakeDmg = false;
    }
}
