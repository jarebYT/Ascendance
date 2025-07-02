using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck; // Un point de vérification au bas du personnage

    private Rigidbody2D body;
    private bool grounded;

    public bool IsGrounded => grounded; // Au sol ?

    private bool canDash = true; // Peut-on dash ?
    public bool isDashing = false; // Est-on en train de dash ?
    [SerializeField] private float dashPower = 20f; // Puissance du dash
    [SerializeField] private float dashTime = 0.2f; // Durée du dash
    private float dashCooldown = 1f; // Temps de recharge du dash

    [SerializeField] public bool isWaking = true; // Le joueur est en train de se réveiller

    AudioManager audioManager;

    private PlayerAnimation animator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<PlayerAnimation>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (!pauseSettings.pause)
        {
            if (isWaking)
            {
                // Met à jour grounded sinon l'animation de saut se joue à tort
                grounded = true;
                isDashing = false; // Désactive le dash pendant qu'on se réveille

                Debug.Log(grounded);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.TriggerStandUp();
                    isWaking = false;
                }

                return;
            }

            // Mouvement horizontal normal
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (!isDashing)
            {
                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            }

            // Flip personnage
            if (horizontalInput > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (horizontalInput < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            // Check au sol
            if (isWaking)
            {
                grounded = true; // Le personnage est au sol pendant qu'il se réveille
            }
            else
            {
                // Vérifie si le personnage est au sol
                grounded = CheckIfGrounded();
            }

            // Saut
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }

            // Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate()
    {
        // Vérification continue si le personnage est au sol
        if (isDashing)
        {
            return; // Ne pas vérifier le sol pendant le dash
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce); // Le saut est appliqué uniquement sur l'axe Y
        grounded = false; // Le personnage n'est plus au sol pendant qu'il saute
    }

    private bool CheckIfGrounded()
    {
        // Utilisation d'un Raycast légèrement sous le personnage pour vérifier le sol
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        
        // Affichage visuel du Raycast pour déboguer dans la scène (facultatif)
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.2f, Color.red);

        // Si le raycast touche un sol, le personnage est considéré comme étant au sol
        return hit.collider != null;
    }

    // Visualisation du point de vérification au sol
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }

    private IEnumerator Dash()
    {

        canDash = false; // Désactive le dash pendant l'exécution
        isDashing = true; // Indique que le personnage est en train de dash
        float originalGravity = body.gravityScale; // Sauvegarde la gravité originale
        body.gravityScale = 0; // Désactive la gravité pendant le dash
        body.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0); // Applique le dash
        yield return new WaitForSeconds(dashTime); // Attend la durée du dash
        body.gravityScale = originalGravity; // Restaure la gravité originale
        isDashing = false; // Le dash est terminé
        yield return new WaitForSeconds(dashCooldown); // Temps de recharge du dash
        canDash = true; // Le dash est à nouveau disponible
    }
}
