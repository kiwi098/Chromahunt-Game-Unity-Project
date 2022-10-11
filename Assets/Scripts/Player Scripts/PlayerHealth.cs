using UnityEngine;
using System.Collections;
using Photon.Pun;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth;
    private Animator anim;
    public bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    PhotonView View;

    private void Awake()
    {
        dead = false;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        View = GetComponent<PhotonView>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        //currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        View.RPC("TakeDamageRPC", RpcTarget.AllBuffered, _damage);

        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            //StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached components
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);
            }
        }
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    [PunRPC]
    void TakeDamageRPC(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        anim.SetTrigger("hurt");
        StartCoroutine(Invunerability());
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void HealMoment(float MaxHeal)
    {
        if (currentHealth < startingHealth)
        {
            View.RPC("HealMomentRPC", RpcTarget.AllBuffered, MaxHeal);
        }
    }

    [PunRPC]
    void HealMomentRPC(float MaxHeal)
    {
        currentHealth = currentHealth + MaxHeal;
        StartCoroutine(HealMomentCoroutine());
    }

    private IEnumerator HealMomentCoroutine()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0, 1, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
    }
}