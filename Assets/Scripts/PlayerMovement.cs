using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxcoll;
    private float JumpCooldown;
    private float horizontalinput;

    PhotonView View;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();

        View = GetComponent<PhotonView>();
        transform.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }

    private void Update()
    {
        if (View.IsMine)
        {
            Move();
        }
    }

    private void Move()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        

        //Flip Player to left and right
        if (horizontalinput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalinput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

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
}

