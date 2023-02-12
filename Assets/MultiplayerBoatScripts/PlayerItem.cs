using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class PlayerItem : MonoBehaviour //this is a attached to a gameobject containing a image and playername.
{
    [SerializeField]
    TMP_Text _playerName;

    public void SetPLayerInfo(Player _player)
    {
        _playerName.text = _player.NickName;
    }
}
