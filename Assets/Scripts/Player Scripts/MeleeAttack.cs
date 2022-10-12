using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MeleeAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    public float attackRate;
    private float nextAttackTime = 0f;

    private Animator anim;
    private PlayerMovement playerMovement;

    PhotonView View;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        View = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (View.IsMine)
        {
            if(Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    SoundEffect.PlaySound("SigneAttack");

                    anim.SetTrigger("attack");
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    
                }
            }
        }
    }

    void Attack()
    {
        //Play an attack animation

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
