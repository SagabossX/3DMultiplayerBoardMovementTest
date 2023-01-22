using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public int playerTurn = 1;
    [SerializeField]
    TMP_Text txtCanvas;
    PhotonView pv;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        txtCanvas.text = "PLayer " +playerTurn.ToString() +"'s Turn";
    }
    public void setPlayerTurn()
    {
        pv.RPC("Rpc_SetPLayerTurn", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Rpc_SetPLayerTurn()
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
