using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EzraHealMoment : MonoBehaviour
{
    public float MaxHeal;
    public float HealCoolDown;
    public float HealTimer;

    private GameObject[] Players;
    public GameObject ClosestPlayer;

    private Animator anim;
    private PlayerMovement playerMovement;

    PhotonView View;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        View = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine)
        {
            Players = GameObject.FindGameObjectsWithTag("Player");

            for (int ctr = 0; ctr < Players.Length; ctr++)
            {
                if (ClosestPlayer == null)
                {
                    if (Players[ctr] != gameObject)
                    {
                        ClosestPlayer = Players[ctr];
                    }
                }
                else if (Vector2.Distance(Players[ctr].transform.position, transform.position) < 
                    Vector2.Distance(ClosestPlayer.transform.position, transform.position))
                {
                    if (Players[ctr] != gameObject)
                    {
                        ClosestPlayer = Players[ctr];
                    }
                }
            }
            HealTimer = HealTimer - Time.deltaTime;

            if (Input.GetMouseButton(0) && HealTimer <= 0 && playerMovement.cantAttack())
            {
                HealPlayers();
                HealTimer = HealCoolDown;
            }
        }
    }

    void HealPlayers()
    {
        if (ClosestPlayer != null)
        {
            anim.SetTrigger("heal");
            ClosestPlayer.GetComponent<PlayerHealth>().HealMoment(MaxHeal);
        }
    }
}
