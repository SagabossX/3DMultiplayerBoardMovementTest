using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomItem : MonoBehaviour //holds the name of the room as a gameobject
{
    [SerializeField]
    TMP_Text _roomName;
    public void SetRoomName(string _roomNameSet)
    {
        _roomName.text = _roomNameSet;
    }
    public void OnClickItem()
    {
        LobbyManager.current.JoinRoom(_roomName.text);
    }
}
