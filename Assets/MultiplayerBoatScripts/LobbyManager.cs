using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_InputField _roomInputField;
    [SerializeField]
    GameObject _lobbyPanel;
    [SerializeField]
    GameObject _roomPanel;
    [SerializeField]
    TMP_Text _roomName;

    [SerializeField]
    RoomItem _roomItemPrefab;
    List<RoomItem> _roomItemsList = new List<RoomItem>();
    [SerializeField]
    Transform _contentObject;

    float _timeBetweenUpdates=1.5f;
    float _nextUpdateTime;

    List<PlayerItem> _playerItemList=new List<PlayerItem>();
    [SerializeField]
    PlayerItem _playerItemPrefab;
    [SerializeField]
    Transform _playerItemTransform;

    [SerializeField]
    GameObject _playButton;

    public static LobbyManager current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }
    public void onClickCreate()
    {
        if (!string.IsNullOrEmpty(_roomInputField.text))
        {
            PhotonNetwork.CreateRoom(_roomInputField.text,new RoomOptions() { MaxPlayers=4}); //create a room on clicking create button setting max players to 4
        }
    }

    public override void OnJoinedRoom()
    {
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text ="Lobby Name: "+ PhotonNetwork.CurrentRoom.Name;  //after joining  room display the room panel and update the player list in the room.
        UpdatePLayerList();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= _nextUpdateTime)
        {
            UpdateRoomList(roomList);                                  //show new rooms when available
            _nextUpdateTime = Time.time + _timeBetweenUpdates;
        }
      
    }
    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in _roomItemsList) //clear all the rooms
        {
            Destroy(item.gameObject);
        }
        _roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
           RoomItem newRoom= Instantiate(_roomItemPrefab, _contentObject); //display the new available rooms
            newRoom.SetRoomName(room.Name);
            _roomItemsList.Add(newRoom);
        }
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        _roomPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    void UpdatePLayerList()
    {
        foreach (PlayerItem item in _playerItemList)
        {
            Destroy(item.gameObject);                         //clear all the player
        }
        _playerItemList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(_playerItemPrefab,_playerItemTransform);   //display players in room
            newPlayerItem.SetPLayerInfo(player.Value);
            _playerItemList.Add(newPlayerItem);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePLayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePLayerList();
    }
    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2) //show play button to master client and when player requirements are met
        {
            _playButton.SetActive(true);
        }
        else
        {
            _playButton.SetActive(false);
        }
    }

    public void OnClickPlayGame()
    {
        PhotonNetwork.LoadLevel("Game"); //load the game scene
    }
}
