using UnityEngine;

public class BossAnimation : MonoBehaviour
{

    private Animator animator;
    private BossScript bossScript;
    private BossMovement bossMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetBool("isWalking", true);
    }

    public void StopWalk()
    {
        animator.SetBool("isWalking", false);
    }

    public void Attack1()
    {
        animator.SetTrigger("attack1");
    }

    public void Attack2()
    {
        animator.SetTrigger("attack2");
    }

    public void Attack3()
    {
        animator.SetTrigger("attack3");
    }
}
