using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
         Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);
        startButton.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
