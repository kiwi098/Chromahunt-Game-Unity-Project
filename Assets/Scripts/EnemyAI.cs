using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed = 10;

    public GameObject[] Players;

    private Rigidbody2D Body;
    private BoxCollider2D BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {

    }
}
