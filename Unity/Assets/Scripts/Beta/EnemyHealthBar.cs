using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider EnemyHealth;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        EnemyHealth = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealth.value = GetComponentInParent<Enemy>().currentHealth; 
        
    }
}
