using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BossHealthManager : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
 
    public HealthBar healthBar;
    // public GameObject bloodEffect;
 
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
 
 
    public void TakeDamage(int damage, Vector2 origin)
    {
        currentHealth -= damage;
 
        // Blood Particle example
        // Instantiate(bloodEffect, transform.position, Quaternion.identity);
        
        // Camera shake code example
        //CameraShake.instance.Shake();
 
        // Knockback code example
        //GetComponent<Rigidbody2D>().AddForce((GetComponent<Rigidbody2D>().position - origin).normalized * 500f, ForceMode2D.Force);
 
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
 
        healthBar.SetCurrentHealth(currentHealth);
    }
}
 