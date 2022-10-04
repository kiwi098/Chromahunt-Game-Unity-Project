using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawnPoint : MonoBehaviour
{
    public int SpawnID;
    public int EnemyCount;
    public int TriggerLimit;

    public GameObject Enemy;

    private int TriggerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnTrigger.TriggerSpawn += SpawnCall;
    }

    public void SpawnCall(int EventID)
    {
        if (EventID == SpawnID)
        {
            if (TriggerCount < TriggerLimit)
            {
                Debug.Log("SPAWN");

                for (int ctr = 0; ctr < EnemyCount; ctr++)
                {
                    Instantiate(Enemy, transform.position, Quaternion.identity);
                }

                TriggerCount = TriggerCount + 1;
            }
        }
    }

    void OnDestroy()
    {
        EnemySpawnTrigger.TriggerSpawn -= SpawnCall;
    }
}
