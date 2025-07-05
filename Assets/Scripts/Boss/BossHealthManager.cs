using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthManager : MonoBehaviour
{
    public GameObject EndText;
    public TMP_Text spareText;
    public TMP_Text destroyText;
    public TMP_Text ChoiceText;
    private BossScript bossScript;

    private BossMovement bossMovement;

    public int maxHealth = 500;
    public int currentHealth;

    public HealthBar healthBar;
    public bool isDefeated = false;

    AudioManager audioManager;

    private BossAnimation animator;

    private void Start()
    {
        bossScript = GetComponent<BossScript>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponentInChildren<BossAnimation>();
    }
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void TakeDamage(int damage, Vector2 origin)
    {
        if (!isDefeated)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                isDefeated = true;
                foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
                {
                    if (script != this)
                    {
                        script.enabled = false;
                    }
                }
                StartCoroutine(EndFight());
            }
        }

    }

    IEnumerator EndFight()
    {   
        animator.FirstDeath();
        yield return new WaitForSeconds(5f);
        EndText.SetActive(true);
        Debug.Log("Appuyez sur F pour retourner au menu principal ou K pour détruire le boss.");
        bool actionTaken = false;
        while (!actionTaken)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                animator.Killed();
                Debug.Log("Le boss a été détruit !");
                ChoiceText.gameObject.SetActive(false);
                destroyText.gameObject.SetActive(true);
                audioManager.PlaySFX(audioManager.chain);
                yield return new WaitForSeconds(5f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
                actionTaken = true;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Retour au menu principal !");
                ChoiceText.gameObject.SetActive(false);
                spareText.gameObject.SetActive(true);
                audioManager.PlaySFX(audioManager.chain);
                yield return new WaitForSeconds(5f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
                actionTaken = true;
            }
            yield return null;
        }


    }
}
 