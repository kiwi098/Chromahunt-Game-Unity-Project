using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public float MoveSpeed;
    public float JumpPower;
    public float DetectRange;

    public Transform attackPoint;
    public float AttackRange;
    public int AttackDamage;
    public float AttackRate;
    private float nextAttackTime = 0f;

    public GameObject SpawnPoint;
    public float PatrolRange;

    public LayerMask groundLayer;
    public LayerMask PlayerLayer;

    private GameObject[] Players;
    private GameObject ClosestPlayer;

    private Rigidbody2D Body;
    private BoxCollider2D BoxCollider;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ClosestPlayer = null;

        Body = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        if (ClosestPlayer != null)
        {
            if (ClosestPlayer.GetComponent<PlayerHealth>().dead == true)
            {
                ClosestPlayer = null;
            }
        }

        for (int ctr = 0; ctr < Players.Length; ctr++)
        {
            if (ClosestPlayer == null)
            {
                if (Players[ctr].GetComponent<PlayerHealth>().dead == false)
                {
                    ClosestPlayer = Players[ctr];
                }
            }
            else if (Vector2.Distance(Players[ctr].transform.position, transform.position) < 
                Vector2.Distance(ClosestPlayer.transform.position, transform.position) &&
                Players[ctr].GetComponent<PlayerHealth>().dead == false)
            {
                ClosestPlayer = Players[ctr];
            }
        }

        transform.eulerAngles = Vector3.zero;
        if (ClosestPlayer != null)
        {
            if (Vector2.Distance(ClosestPlayer.transform.position, transform.position) < DetectRange)
            {
                ChasePlayer();
                AttackPlayer();
            }
            else
            {
                animator.SetFloat("speed", -1.0f);
                Patrol();
            }
        }
        else
        {
            animator.SetFloat("speed", -1.0f);
            Patrol();
        }
        Death();
    }

    void Patrol()
    {
        
    }

    void ChasePlayer()
    {
        float InputX = 0;
        float InputY = Body.velocity.y;

        if ((ClosestPlayer.transform.position - transform.position).x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if (AttackRange*2.2f < Vector2.Distance(ClosestPlayer.transform.position, transform.position))
        {
            if (Vector2.Distance(new Vector2(ClosestPlayer.transform.position.x, 0), new Vector2(transform.position.x, 0)) >=
                Vector2.Distance(new Vector2(0, ClosestPlayer.transform.position.y), new Vector2(0, transform.position.y)))
            {
                if ((ClosestPlayer.transform.position - transform.position).x < 0)
                {
                    InputX = -1 * MoveSpeed;
                }
                else
                {
                    InputX = 1 * MoveSpeed;
                }
                animator.SetFloat("speed", 1.0f);
            }
            else
            {
                if (isGrounded())
                {
                    if (ClosestPlayer.transform.position.y > transform.position.y)
                    {
                        Body.gravityScale = 7;
                        InputY = 1 * JumpPower;
                    }
                }
            }
        }
        else
        {
            animator.SetFloat("speed", -1.0f);
        }

        Body.velocity = new Vector2(InputX, InputY);
    }

    void AttackPlayer()
    {
        if (AttackRange*2.2f >= Vector2.Distance(ClosestPlayer.transform.position, transform.position))
        {
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("attack");

                Collider2D[] HitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange/2, PlayerLayer);

                foreach(Collider2D Player in HitPlayers)
                {
                    Player.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);
                }
                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
    }

    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died!");
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("ouch");
        currentHealth -= damage;
        Death();
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }
}
