using System.Collections;
using UnityEngine;

public class BossMobility : MonoBehaviour
{
    public Transform player;
    private BossScript bossScript;
    public float alertRange = 6f;   // Zone d'alerte, boss idle mais prêt
    public float chaseRange = 0f;    // Zone de chase activée après alerte
    public bool isWakingUp = false;


    public float moveSpeed = 3f;


    private Rigidbody2D rb;
    private bool canChase = false;   // Est-ce que le boss peut chasser ?
    
    

    IEnumerator SleepCoroutine()
    {
        Debug.Log("Le boss a vu le joueur, il attend 2s avant de bouger...");
        yield return new WaitForSeconds(2f);

        canChase = true;
        alertRange = 0f;
        chaseRange = 20f;
        Debug.Log("2 secondes écoulées — le boss se met à chasser !");
    }




    void Start()
    {
        bossScript = GetComponent<BossScript>();
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            Debug.LogError("Référence au joueur non assignée !");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if (!canChase && !isWakingUp && distanceToPlayer <= alertRange)
        {
            // Joueur vu dans la zone d'alerte : on active la chasse
            isWakingUp = true;
            StartCoroutine(SleepCoroutine());
            // Ici tu peux déclencher une animation "réveil" ou jouer un son, etc.
        }

        if (canChase && distanceToPlayer <= chaseRange && bossScript.canMove)
        {
            // Le boss suit le joueur
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 horizontalDirection = new Vector2(direction.x, 0);
            Vector2 newVelocity = horizontalDirection * moveSpeed;
            newVelocity.y = rb.linearVelocity.y;
            rb.linearVelocity = newVelocity;

            Flip(player.position.x > transform.position.x);

            // Ici tu peux lancer une animation "run"
            animator.Walk();
        }
        else if (!canChase && distanceToPlayer <= alertRange)
        {
            // Boss en alerte (idle) regarde le joueur sans bouger
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            Flip(player.position.x > transform.position.x);
            // Animation idle ou "alert"
            animator.StopWalk();
        }
        else
        {
            // Joueur trop loin, boss idle sans regarder le joueur
            rb.linearVelocity = Vector2.zero;
            
            // Animation idle classique
            animator.StopWalk();
        }
    }

    void Flip(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
