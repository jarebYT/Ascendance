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


    private void Start()
    {
        bossScript = GetComponent<BossScript>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
                bossScript.canMove = false; // Stop the boss from moving
                bossScript.canAttack = false; // Stop the boss from attacking
                StartCoroutine(EndFight());
            }
        }
        
    }

    IEnumerator EndFight()
    {
        yield return new WaitForSeconds(5f);
        EndText.SetActive(true);
        Debug.Log("Appuyez sur F pour retourner au menu principal ou K pour détruire le boss.");
        bool actionTaken = false;
        while (!actionTaken)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Le boss a été détruit !");
                ChoiceText.gameObject.SetActive(false);
                destroyText.gameObject.SetActive(true);
                yield return new WaitForSeconds(5f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
                actionTaken = true;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Retour au menu principal !");
                ChoiceText.gameObject.SetActive(false);
                spareText.gameObject.SetActive(true);
                yield return new WaitForSeconds(5f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
                actionTaken = true;
            }
            yield return null;
        }


    }
}
 