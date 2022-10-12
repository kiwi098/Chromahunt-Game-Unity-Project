using Photon.Pun;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTImer = Mathf.Infinity;

    PhotonView View;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        View = GetComponent<PhotonView>();
        FindArrows();
    }

    private void FindArrows()
    {
        arrows = GameObject.FindGameObjectsWithTag("Bullet");

        for (int ctr = 0; ctr < arrows.Length; ctr++)
        {
            arrows[ctr].SetActive(false);
        }
    }

    private void Update()
    {
        if (View.IsMine)
        {
       
            if (Input.GetMouseButton(0) && cooldownTImer > attackCooldown && playerMovement.cantAttack())

            Attack();
            cooldownTImer += Time.deltaTime;
        }
    }
    private void Attack()
    {
        SoundEffect.PlaySound("GiannisAttack");
        
        anim.SetTrigger("attack");
        cooldownTImer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        //pool fireballs


        //anim.ResetTrigger("attack");
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
