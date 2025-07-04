using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BossHealthManager : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
 
    public HealthBar healthBar;
 
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
 
 
    public void TakeDamage(int damage, Vector2 origin)
    {
        currentHealth -= damage;
 
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
 
        healthBar.SetCurrentHealth(currentHealth);
    }
}
 