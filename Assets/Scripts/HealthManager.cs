using UnityEngine;
 
// Just a simple health manager to show how you can controll the HealthBar UI
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
        // Damage player when we press the G key
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
 