using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_InputField _username;          //Get UserName for player
    [SerializeField]
    TMP_Text _buttonTxt;  

    public void OnLoginClicked()
    {
        if (!string.IsNullOrEmpty(_username.text))  //when login button clicked if username is not empty connect to photon servers using the username.
        {
            PhotonNetwork.NickName = _username.text;
            _buttonTxt.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(1);  //Load lobby scene when connected to server
    }
}
