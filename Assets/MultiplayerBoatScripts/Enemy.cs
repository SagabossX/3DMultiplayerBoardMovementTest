using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float health;
    float max_health;
    PhotonView view;

    public int _playerThatKilled;
    [SerializeField]
    Canvas healthcanvas;
    [SerializeField]
    Slider healthbar;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
        health = 100 * enemySpawner.current.wave;             //set a health that scales with the wave number
        max_health = health;
        healthcanvas.worldCamera = Camera.main;
    }
    
    public void TakeDamage(int damage,int actornumber)   //on taking damage from a bullet call and rpc that passes the damage and the player id to all players.
    {
        view.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage,actornumber);
    }
    [PunRPC]
    public void RPC_TakeDamage(int _damage,int actornumber)
    {
        if (health - _damage > 0)
        {
            health = health - _damage;
        }
        else if (health - _damage <= 0)
        {
            _playerThatKilled = actornumber;  //check wich player id is responsible for destroying enemy
            DestroyEnemy();
        }
        
    }

    void DestroyEnemy()
    {
        

        if (view.IsMine)
        {
            enemySpawner.current.EnemyKilled(this); //pass the enemy as reference and call the action so we acess who killed the enemy .
            PhotonNetwork.Destroy(view); //destroy the enemy
        }
       
    }

    private void FixedUpdate()
    {
       transform.position= Vector3.MoveTowards(transform.position, new Vector3(0, 2, -13), 1f * Time.fixedDeltaTime); //move enemy towrd a certain point
       
    }
    private void Update()
    {
        healthbar.value = (health / max_health);
        if(transform.position== new Vector3(0, 2, -13)) //set healthbar and check if we reached our destination that is the player base
        {
            BaseHealth.current.Onshiphit();
            if (view.IsMine)
            {
                _playerThatKilled = 10;                   //set playerid to 10 so we can check and give credit to no one ,by returning if value is 10.
                enemySpawner.current.EnemyKilled(this);
                PhotonNetwork.Destroy(view);
            }
        }
    }
}
