using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private PlayerMovement playerMovement;

    public HealthBar healthBar;

    private PlayerAnimation playerAnimation;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetCurrentHealth(currentHealth);
        Debug.Log($"Joueur prend {damage} dégâts. Santé restante: {currentHealth}");
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        playerMovement.canMove = false; // Désactiver le mouvement du joueur
                                        // Désactiver tous les scripts sauf HealthManager
        playerAnimation.TriggerDeath(); // Déclencher l'animation de mort
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }
        GameObject boss = GameObject.FindWithTag("enemy");
        if (boss != null)
        {
            foreach (MonoBehaviour script in boss.GetComponents<MonoBehaviour>())
            {
                script.enabled = false;
            }
        }
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
    }
}
 