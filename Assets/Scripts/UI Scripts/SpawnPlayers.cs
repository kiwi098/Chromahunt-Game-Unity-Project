using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject CM_Vcam;

    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {

         int randomNumber = Random.Range(0, spawnPoints.Length);
         Transform spawnPoint = spawnPoints[randomNumber];
         GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
         Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);
         GameObject SpawnedPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        CM_Vcam.GetComponent<CinemachineVirtualCamera>().Follow = SpawnedPlayer.transform;
     }
    }

