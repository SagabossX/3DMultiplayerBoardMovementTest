using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class SpawnPlayer : MonoBehaviour
{
   
    void Start()
    {
        CreatePlayer();
    }

  
    void CreatePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Playa"), Vector3.zero, Quaternion.identity) as GameObject; //spawn player on scene.
    }
}
