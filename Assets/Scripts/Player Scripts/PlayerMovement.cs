using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxcoll;
    private float JumpCooldown;
    private float horizontalinput;

    PhotonView View;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        View = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (View.IsMine)
        {
            transform.eulerAngles = Vector3.zero;
            Move();
        }
    }

    private void Move()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        

        //Flip Player to left and right
        if (horizontalinput > 0.01f){
            transform.localScale = Vector3.one;
            animator.SetFloat("speed", 1.0f);   //player is moving,  play walk animation
        }else if (horizontalinput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("speed", 1.0f);   //player is moving, play walk animation
        }else{
            animator.SetFloat("speed", -1.0f);  //player stopped, play idle
        }
        //Jump Logic
        if (JumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalinput * speed, body.velocity.y);
            if (isGrounded() && Input.GetKey(KeyCode.Space))
            {
                body.gravityScale = 7;
                Jump();
            }
        }
        else
            JumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
            JumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    void Death()
    {
        if (CurrentHealth <= 0)
        {
            Debug.Log("Player died!");
            //Destroy(gameObject);
        }
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Death();
    }
    public bool cantAttack()
    {
        return true;
    }
}

