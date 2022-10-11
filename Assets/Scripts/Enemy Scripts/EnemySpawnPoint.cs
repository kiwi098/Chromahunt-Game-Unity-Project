using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class EnemySpawnPoint : MonoBehaviour
{
    public int SpawnID;
    public int EnemyCount;
    public int TriggerLimit;

    public GameObject Enemy;
    private GameObject Spawned;

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
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("SPAWN");

                    for (int ctr = 0; ctr < EnemyCount; ctr++)
                    {
                        //Spawned = Instantiate(Enemy, transform.position, Quaternion.identity);
                        Spawned = PhotonNetwork.InstantiateRoomObject(Enemy.name, transform.position, Quaternion.identity);
                        Spawned.GetComponent<EnemyAI>().SpawnPoint = transform.gameObject;
                    }

                    TriggerCount = TriggerCount + 1;
                }
            }
        }
    }

    void OnDestroy()
    {
        EnemySpawnTrigger.TriggerSpawn -= SpawnCall;
    }
}
