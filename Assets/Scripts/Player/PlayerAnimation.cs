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
        if (playerMovement.isWaking) return;

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

    // Appelé à la fin d’animation "WakeUp" pour appliquer l'animation "WakeUpIdle"
    // et permettre au joueur de sauter uniquement
    public void WakeUpFinished()
    {
        anim.Play("WakeUpIdle");
        playerMovement.canMove = true;
    }

    // Appelé lorsque le joueur appuie sur Espace pour se lever et termine l'idle
    public void TriggerStandUp()
    {
        anim.SetTrigger("standUp");
        anim.SetBool("canMove", true);

    }

    // Dans PlayerMovement pour bloquer le mouvement pendant le idle réveil
    public bool IsInWakeUpIdle()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("WakeUpIdle");
    }

    public void TriggerDeath()
    {
        anim.SetTrigger("isDead");
        anim.SetBool("boolIsDead", true);
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    
}
