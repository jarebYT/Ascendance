using UnityEngine;
 
public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
 
    public HealthBar healthBar;
 
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
        }
    }
 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
 
        healthBar.SetCurrentHealth(currentHealth);
    }
}
 