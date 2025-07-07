using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform player;
    private BossScript bossScript;
    public float alertRange = 6f;   // Zone d'alerte, boss idle mais prêt
    public float chaseRange = 0f;    // Zone de chase activée après alerte
    public bool isWakingUp = false;
    public  AudioManager audioManager;

    private BossAnimation animator;


    public float moveSpeed = 3f;

    public GameObject healthBar;


    private Rigidbody2D rb;
    private bool canChase = false;   // Est-ce que le boss peut chasser ?
    
    
    private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    IEnumerator SleepCoroutine()
    {
        healthBar.SetActive(true);
        audioManager.MusicStop();
        audioManager.PlayMusicLoop(audioManager.music_boss);

        // On dit à la coroutine de se mettre en pause pendant 2s 
        yield return new WaitForSeconds(2f);

        // On active le fait que le boss puisse chasser le joueur et ensuite on vient mettre à 0 l'alertRange et augmenter la range de suivi pour que le joueur ne puisse pas s'en échapper
        canChase = true;
        alertRange = 0f;
        chaseRange = 20f;

    }




    void Start()
    {
        // On vient récupérer les composant sur le GameObject
        animator = GetComponentInChildren<BossAnimation>();
        bossScript = GetComponent<BossScript>();
        rb = GetComponent<Rigidbody2D>();

        // On check si la variable "player" est vide pour évité que le script se lance pour rien
        if (player == null)
        {
            Debug.LogError("Référence au joueur non assignée !");
            enabled = false;
        }
    }


    void FixedUpdate()
    {
        // On vient calculer la distance du boss au joueur avec le "transform.position" on vient récupérer la position du boss
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        // Ici on check si le boss ne peut pas chasser le joueur et si le boss n'est pas réveillé et est ce que le joueur est dans la zone d'alerte
        if (!canChase && !isWakingUp && distanceToPlayer <= alertRange)
        {
            // Joueur vu dans la zone d'alerte : on active la chasse
            isWakingUp = true;
            
            // On lance la coroutine qu'on a défini plutôt
            StartCoroutine(SleepCoroutine());
        }

        // Ici on check si le boss peut chasser et que le joueur est dans la zone de range ET que le boss peut bouger
        if (canChase && distanceToPlayer <= chaseRange && bossScript.canMove)
        {
            // Le boss suit le joueur

            // On cherche à savoir dans quel direction est le joueur, le ".normalized" permet de savoir si il est soit à gauche soit à droite sans parler de vitesse mais juste la direction
            Vector2 direction = (player.position - transform.position).normalized;
            // Ici on ignore l'axe Y car sinon le boss vole dans les airs en suivant le boss si il saute donc on veut juste le x
            Vector2 horizontalDirection = new Vector2(direction.x, 0);

            // On donne juste la direction et la moveSpeed qu'on a déclarer au tout début ça permet de fixer la vitesse du boss
            Vector2 newVelocity = horizontalDirection * moveSpeed;

            // Ici on vient appliquer notre Velocity au Rigidbody et donc le boss se déplace selon ce qu'il lui a été donné
            rb.linearVelocity = newVelocity;

            // Ici une fonction pour dire de changer le sprite du boss de sens si il est pas 
            Flip(player.position.x > transform.position.x);

            // Ici on lance l'animation de marche du boss
            animator.Walk();
        }

        // On check si le boss peut chasser et si le joueur est dans la range d'alerte du boss 
        else if (!canChase && distanceToPlayer <= alertRange)
        {

            // On enleve tout mouvement en mettant tout à zero
            rb.linearVelocity = Vector2.zero;

            // Ici une fonction pour dire de changer le sprite du boss de sens si il n'y est pas 
            Flip(player.position.x > transform.position.x);
            
            // On stop l'animation de marche
            animator.StopWalk();
        }
        else
        {
            // On enleve tout mouvement en mettant tout à zero
            rb.linearVelocity = Vector2.zero;
            
            // Animation idle classique
            animator.StopWalk();
        }
    }


    // Cette fonction permet retourner le sprite du boss
    void Flip(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        // Condition pour changer le sens du sprite donc si le boolean est faux il va changer le sens vers la gauche et si le boolean est vrai il va mettre le sens vers la droite
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    // Cette fonction sert juste à afficher les hitbox de la range d'alerte et range de chasse
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
