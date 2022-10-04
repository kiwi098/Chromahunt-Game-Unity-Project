using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawnTrigger : MonoBehaviour
{
    public int TriggerID;

    public static event Action<int> TriggerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("TRIGGER");
            TriggerSpawn?.Invoke(TriggerID);
        }
    }
}
