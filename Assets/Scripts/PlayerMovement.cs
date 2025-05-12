using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck; // Un point de vérification au bas du personnage

    private Rigidbody2D body;
    private bool grounded;

    public bool IsGrounded => grounded; // Propriété publique pour vérifier si le personnage est au sol

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Mouvement horizontal
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); // Utilise `velocity` pour un mouvement instantané

        // Flip du personnage
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Vérifier si le personnage est au sol
        grounded = CheckIfGrounded();

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
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
}
