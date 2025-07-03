using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    [Header("Events")]
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnPlayerDeath;
    
    [Header("Invincibility")]
    [SerializeField] private float invincibilityTime = 1f;
    private bool isInvincible = false;

    private PlayerAnimation animator;
    
    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        if (isInvincible) return;
        
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        
        Debug.Log($"Joueur prend {damage} dégâts. Santé restante: {currentHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }
    
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        
        Debug.Log($"Joueur récupère {healAmount} points de santé. Santé actuelle: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("Le joueur est mort !");
        OnPlayerDeath?.Invoke();
        animator.TriggerDeath();
        
        // Ici vous pouvez ajouter la logique de mort du joueur
        // Par exemple : respawn, game over screen, etc.
    }
    
    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        
        // Effet visuel d'invincibilité (clignotement optionnel)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float timer = 0;
            while (timer < invincibilityTime)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(1, 1, 1, 1f);
                yield return new WaitForSeconds(0.1f);
                timer += 0.2f;
            }
        }
        else
        {
            yield return new WaitForSeconds(invincibilityTime);
        }
        
        isInvincible = false;
    }
    
    // Getters pour l'UI ou d'autres scripts
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => currentHealth / maxHealth;
    public bool IsAlive() => currentHealth > 0;
}
