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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Heal();
        }
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

    private void Heal()
    {
    currentHealth += 100;
    healthBar.SetCurrentHealth(currentHealth);
    Debug.Log($"Joueur guérit. Santé actuelle: {currentHealth}");
    }

    IEnumerator Die()
    {
        playerMovement.canMove = false;
                                        
        playerAnimation.TriggerDeath();
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
 