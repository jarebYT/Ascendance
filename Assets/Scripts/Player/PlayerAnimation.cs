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
        if (playerMovement.isWaking) return; // Ne touche à rien si on est en train de se réveiller

        if (playerMovement.isDashing)
        {
            anim.SetBool("isDashing", true);
            return;
        }

        anim.SetBool("isDashing", false);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("run", horizontalInput != 0);

        anim.SetBool("grounded", playerMovement.IsGrounded);
    }




    public void TriggerStandUp()
    {
        anim.SetTrigger("standUp");
        anim.SetBool("canMove", true); // Maintenant on peut bouger

    }

    // Appelé depuis l’event de fin d’animation "WakeUp"
    public void OnWakeUpFinished()
    {
        anim.Play("WakeUpIdle"); // Force le perso à rester à 4 pattes
    }
}
