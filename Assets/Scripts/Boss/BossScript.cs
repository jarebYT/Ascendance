using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Attacks")]
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int attack1Damage = 20;    // Attaque 1
    [SerializeField] private int attack2Damage = 25;    // Attaque 2
    [SerializeField] private int attack3Damage = 30;    // Attaque 3
    
    [Header("Attack Timing")]
    [SerializeField] private float attack1Duration = 0.5f;
    [SerializeField] private float attack2Duration = 0.7f;
    [SerializeField] private float attack3Duration = 0.9f;
    
    public bool canAttack = true;
    private bool isAttacking = false;

    public bool canMove = true; // Pour contrôler si le boss peut se déplacer ou non

    private BossAnimation animator;

    void Start()
    {
        animator = GetComponentInChildren<BossAnimation>();
    }

    void Update()
    {
        if (player != null && canAttack && !isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Vérifier si le joueur est dans la portée d'attaque
            if (distanceToPlayer <= attackRange)
            {
                ExecuteRandomAttack();
            }
        }
    }
    
    void ExecuteRandomAttack()
    {
        int randomAttack = Random.Range(0, 3);
        
        switch (randomAttack)
        {
            case 0:
                StartCoroutine(ExecuteAttack1());
                break;
            case 1:
                StartCoroutine(ExecuteAttack2());
                break;
            case 2:
                StartCoroutine(ExecuteAttack3());
                break;
        }
    }

    IEnumerator ExecuteAttack1()
    {
        isAttacking = true;
        canAttack = false;
        canMove = false; // Le boss ne peut pas se déplacer pendant l'attaque

        Debug.Log("Boss exécute l'attaque 1 !");


        yield return new WaitForSeconds(attack1Duration * 0.6f); // Délai avant impact

        // Vérifier si le joueur est encore dans la portée
        float currentDistance = Vector2.Distance(transform.position, player.position);
        if (currentDistance <= attackRange)
        {
            DealDamageToPlayer(attack1Damage);
            Debug.Log($"Attaque 1 inflige {attack1Damage} dégâts !");
        }

        yield return new WaitForSeconds(attack1Duration * 0.4f); // Fin de l'animation
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
        canMove = true; // Le boss peut se déplacer à nouveau
    }

    IEnumerator ExecuteAttack2()
    {
        isAttacking = true;
        canAttack = false;
        canMove = false; // Le boss ne peut pas se déplacer pendant l'attaque

        Debug.Log("Boss exécute l'attaque 2 !");

        yield return new WaitForSeconds(attack2Duration * 0.6f); // Délai avant impact

        // Vérifier si le joueur est encore dans la portée
        float currentDistance = Vector2.Distance(transform.position, player.position);
        if (currentDistance <= attackRange)
        {
            DealDamageToPlayer(attack2Damage);
            Debug.Log($"Attaque 2 inflige {attack2Damage} dégâts !");
        }

        yield return new WaitForSeconds(attack2Duration * 0.4f); // Fin de l'animation
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
        canMove = true; // Le boss peut se déplacer à nouveau
    }

    IEnumerator ExecuteAttack3()
    {
        isAttacking = true;
        canAttack = false;
        canMove = false; // Le boss ne peut pas se déplacer pendant l'attaque

        Debug.Log("Boss exécute l'attaque 3 !");

        yield return new WaitForSeconds(attack3Duration * 0.6f); // Délai avant impact

        // Vérifier si le joueur est encore dans la portée
        float currentDistance = Vector2.Distance(transform.position, player.position);
        if (currentDistance <= attackRange)
        {
            DealDamageToPlayer(attack3Damage);
            Debug.Log($"Attaque 3 inflige {attack3Damage} dégâts !");
        }

        yield return new WaitForSeconds(attack3Duration * 0.4f); // Fin de l'animation
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
        canMove = true; // Le boss peut se déplacer à nouveau
    }
    
    void DealDamageToPlayer(int damage)
    {
        HealthManager playerHealth = player.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
    
    // Méthode pour visualiser les portées dans l'éditeur
    void OnDrawGizmosSelected()
    {
        // Portée d'attaque générale
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}