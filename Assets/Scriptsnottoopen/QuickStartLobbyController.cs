using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickstartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int roomsize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickstartButton.SetActive(true);
    }
    public void QuickStart()
    {
        quickstartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("quickstart executed");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("failed to join room");
        CreateRoom();
    }
    void CreateRoom()
    {
        Debug.Log("Creating Room");
        int randomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomsize };
        PhotonNetwork.CreateRoom("Room" + randomNumber, roomOps);
        Debug.Log("Room created with room number: " + randomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room..creating room again");
        CreateRoom();
    }
    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickstartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
