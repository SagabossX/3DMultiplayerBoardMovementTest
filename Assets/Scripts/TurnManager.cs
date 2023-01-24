using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
public class TurnManager : MonoBehaviour
{
    public static TurnManager current;
    public int playerTurn = 1;
    [SerializeField]
    TMP_Text txtCanvas;  // reference to text obj to display player turn
    PhotonView pv;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        MovePlayer.current.OnPlayerFinishedMove += setPlayerTurn;   //Listen to action fired by moveplayer script.
        pv = GetComponent<PhotonView>();
        txtCanvas.text = "PLayer " +playerTurn.ToString() +"'s Turn";
    }
    public void setPlayerTurn()                //Call rpc
    {
        pv.RPC("Rpc_SetPLayerTurn", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Rpc_SetPLayerTurn()             //set player turn for all players in network.
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
        }
        txtCanvas.text = "PLayer " + playerTurn.ToString() + "'s Turn";
    }
}
