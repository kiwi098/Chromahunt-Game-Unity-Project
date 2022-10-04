using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxcoll;
    private float JumpCooldown;
    private float horizontalinput;


    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        

        //Flip Player to left and right
        if (horizontalinput > 0.01f){
            transform.localScale = Vector3.one;
            animator.SetFloat("speed", 1f);
        }else if (horizontalinput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("speed", 1f);
        }else{
            animator.SetFloat("speed", -1f);
        }

        //Jump Logic
        if (JumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalinput * speed, body.velocity.y);
            if (isGrounded() && Input.GetKey(KeyCode.Space))
            {
                Debug.Log("BRUH");
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
}

