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
    public  AudioManager audioManager;
    
    public bool canAttack = true;
    private bool isAttacking = false;
    private Rigidbody2D rb;


    public bool canMove = true; // Pour contrôler si le boss peut se déplacer ou non

    private BossAnimation animator;

     private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        audioManager.PlaySFX(audioManager.boss_attack); // Joue le son de l'attaque

        isAttacking = true;
        canAttack = false;
        canMove = false; // Le boss ne peut pas se déplacer pendant l'attaque

        Debug.Log("Boss exécute l'attaque 1 !");
        animator.Attack1(); // Joue l'animation de l'attaque 1


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
        audioManager.PlaySFX(audioManager.boss_attack); // Joue le son de l'attaque

        isAttacking = true;
        canAttack = false;
        canMove = false; // Le boss ne peut pas se déplacer pendant l'attaque

        Debug.Log("Boss exécute l'attaque 2 !");
        animator.Attack2(); // Joue l'animation de l'attaque 2


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
        audioManager.PlaySFX(audioManager.boss_attack);

        isAttacking = true;
        canAttack = false;
        canMove = false;

        Debug.Log("Boss exécute l'attaque 3 !");
        animator.Attack3();

        // Petite pause avant le dash
        yield return new WaitForSeconds(attack3Duration * 0.2f);

        Vector2 startPos = rb.position;
        Vector2 targetPos = player.position;

        // On garde la même position Y que le boss au départ
        targetPos.y = startPos.y;

        Vector2 direction = (targetPos - startPos).normalized;
        float slideDistance = 10f;
        float slideDuration = 0.5f;

        Vector2 endPos = startPos + direction * slideDistance;

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            // On interpole la position en gardant Y constant
            Vector2 newPos = Vector2.Lerp(startPos, endPos, elapsed / slideDuration);
            newPos.y = startPos.y; // verrouille la hauteur

            rb.MovePosition(newPos);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(endPos);

        // Pause avant impact
        yield return new WaitForSeconds(attack3Duration * 0.4f);

        float currentDistance = Vector2.Distance(transform.position, player.position);
        if (currentDistance <= attackRange)
        {
            DealDamageToPlayer(attack3Damage);
            Debug.Log($"Attaque 3 inflige {attack3Damage} dégâts !");
        }

        yield return new WaitForSeconds(attack3Duration * 0.4f);
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
        canMove = true;
    }

    void DealDamageToPlayer(int damage)
    {
        HealthManager playerHealth = player.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            audioManager.PlaySFX(audioManager.hero_hurt); // Joue le son de dégâts du joueur
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