using System.Collections;
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

    private void Start()
    {
        PlayerRB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        Prisoner = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentStamina >= attackStamina)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        HealthRegen();
        StaminaRegen();
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
        currentStamina -= attackStamina;
        staminaBar.SetStamina(currentStamina);

        //Play attack animation
        animator.SetTrigger("Attack");

        //Detect enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            hitSound.Play();
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
    void CreateThrustParticles()
    {
        ThrustParticles.Play();
    }

 
}
