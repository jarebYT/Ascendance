using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private Rigidbody2D body;

    // PARTIE VERIF DU SOL
    public bool grounded;
    public bool IsGrounded => grounded;
    //****************************


    // PARTIE DASH
    private bool canDash = true;
    public bool isDashing = false;
    [SerializeField] private float dashPower = 20f;
    [SerializeField] private float dashTime = 0.2f;
    private float dashCooldown = 1f;
    //****************************



    // PARTIE REVEIL
    [SerializeField] public bool isWaking = true;
    public bool canMove = false;
    //****************************

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
                grounded = true;
                isDashing = false;

                // Si on est en WakeUpIdle, on autorise le saut uniquement
                if (animator.IsInWakeUpIdle() && Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    isWaking = false;    
                    canMove = true;      
                    animator.TriggerStandUp();
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

            // Vérifie si le personnage est au sol via raycast
            grounded = CheckIfGrounded();
            

            // Saut
            if (Input.GetKeyDown(KeyCode.Space) && grounded && canMove)
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

        if (isDashing)
        {
            return;
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
    }

    private bool CheckIfGrounded()
    {
        // Raycast légèrement sous le personnage pour vérifier le sol
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        
        
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.2f, Color.red);

        return hit.collider != null;
    }

    private IEnumerator Dash()
    {

        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        body.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0);
        yield return new WaitForSeconds(dashTime);
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
