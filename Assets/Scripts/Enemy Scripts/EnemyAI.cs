using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float MoveSpeed;
    public float AttackRange;
    public float JumpPower;
    public LayerMask groundLayer;

    private GameObject[] Players;
    private GameObject ClosestPlayer;

    private Rigidbody2D Body;
    private BoxCollider2D BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ClosestPlayer = null;

        Body = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        for (int ctr = 0; ctr < Players.Length; ctr++)
        {
            if (ClosestPlayer == null)
            {
                ClosestPlayer = Players[ctr];
            }
            else if (Vector2.Distance(Players[ctr].transform.position, transform.position) < 
                Vector2.Distance(ClosestPlayer.transform.position, transform.position))
            {
                ClosestPlayer = Players[ctr];
            }
        }

        transform.eulerAngles = Vector3.zero;
        ChasePlayer();
        AttackPlayer();
        Death();
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

        if (AttackRange < Vector2.Distance(ClosestPlayer.transform.position, transform.position))
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

        Body.velocity = new Vector2(InputX, InputY);
    }

    void AttackPlayer()
    {
        if (AttackRange < Vector2.Distance(ClosestPlayer.transform.position, transform.position))
        {

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
        currentHealth -= damage;
        Death();
    }
}
