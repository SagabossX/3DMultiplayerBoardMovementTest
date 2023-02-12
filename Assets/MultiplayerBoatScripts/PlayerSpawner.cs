using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] _spawnPoints;

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2) //Spawn the player in middle 2 rows if players is equal to 2
        {
            GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), _spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, Quaternion.identity) as GameObject;
        }
        else //spawn players from the starting spawnpoint in a row
        {
            GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), _spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].position, Quaternion.identity) as GameObject;
        }
        
        
    }
}
