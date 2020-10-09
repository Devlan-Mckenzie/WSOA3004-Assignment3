using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider EnemyHealth;
    public GameObject ShadowAlive;
    public GameObject ShadowDead;

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

        if (EnemyHealth.value <= 0)
        {
            EnemyHealth.gameObject.SetActive(false);
            ShadowAlive.SetActive(false);
            ShadowDead.SetActive(true);           
        }
        
    }
}
