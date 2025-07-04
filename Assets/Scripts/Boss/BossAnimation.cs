using UnityEngine;

public class BossAnimation : MonoBehaviour
{

    private Animator animator;
    private BossScript bossScript;
    private BossMobility bossMobility;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        bossScript = GetComponentInParent<BossScript>();
        bossMobility = GetComponentInParent<BossMobility>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
