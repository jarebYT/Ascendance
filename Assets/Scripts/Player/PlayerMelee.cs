using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMelee : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
 
    public  AudioManager audioManager;
    private float cooldownTimer = 0f;
 
    public int attackDamage = 25;

    public float cooldown = 1f;
 
    public PlayerAnimation animator;

    PlayerMovement playerMovement;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<PlayerAnimation>();

    }
 
    private void Update()
    {
        if (cooldownTimer <= 0 && !playerMovement.isWaking && !playerMovement.isDashing && playerMovement.grounded)
        {
            if (Input.GetKey(KeyCode.K))
            {
                animator.Attack();
                audioManager.PlaySFX(audioManager.hero_attack);
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);
                foreach (var enemy in enemiesInRange)
                {
                    enemy.GetComponent<BossHealthManager>().TakeDamage(attackDamage, transform.position);
                    audioManager.PlaySFX(audioManager.boss_hurt);
                }

                cooldownTimer = cooldown;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}