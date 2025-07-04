using UnityEngine;

public class BossAnimation : MonoBehaviour
{

    private Animator animator;
    private BossScript bossScript;
    private BossMobility bossMobility;

    void Start()
    {
        animator = GetComponent<Animator>();
        bossScript = GetComponentInParent<BossScript>();
        bossMobility = GetComponentInParent<BossMobility>();
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
