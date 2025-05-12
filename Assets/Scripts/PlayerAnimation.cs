using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        // Animation de course
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("run", horizontalInput != 0);

        // Animation de saut (false si au sol)
        anim.SetBool("grounded", playerMovement.IsGrounded);
    }
}
