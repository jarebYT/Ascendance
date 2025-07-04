using UnityEngine;

public class BossAnimation : MonoBehaviour
{

    private Animator animator;
    private BossScript bossScript;
    private BossMobility bossMobility;

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
}
