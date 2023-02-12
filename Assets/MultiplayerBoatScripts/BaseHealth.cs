using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class BaseHealth : MonoBehaviour
{
    public static BaseHealth current;  //Contains health of the base
    float health = 100;
    float maxHealth;
    [SerializeField]
    GameObject GameOverCanvas;
    [SerializeField]
    Slider baseHealthSlider;
    PhotonView view;
    private void Awake()
    {
        current = this;
        maxHealth = health;
        view = GetComponent<PhotonView>();
    }
    public void Onshiphit() //called when a enemy ship reaches the base on all players base
    {
        if (PhotonNetwork.IsMasterClient && view.IsMine)
        {
            view.RPC("RPC_Onshiphit", RpcTarget.AllBuffered);
        }
        
    }

    [PunRPC]
    public void RPC_Onshiphit()
    {
        if (health - 10 > 0)
        {
            health = health - 10;
        }
        else
        {
            GameOver();
        }
       
        
    }
    public void GameOver() //if the base health reaches below 0 we set the lose canvas and do the gameover sequence.
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    private void Update()
    {
        baseHealthSlider.value = health / maxHealth; //set the health bar of the player base
    }
}
