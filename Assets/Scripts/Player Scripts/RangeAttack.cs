
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTImer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTImer > attackCooldown && playerMovement.cantAttack())
            Attack();
        cooldownTImer += Time.deltaTime;
    }
    private void Attack()
    {
        //anim.SetTrigger("attack");
        cooldownTImer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        //pool fireballs
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
