using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Shoot : MonoBehaviour
{
    PhotonView view;

    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    Transform _bulletOrigin;

    [SerializeField]
    float _bulletSpeed;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                view.RPC("ShootBullet", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber); //check for mouse press and call rpc
            }
        }
    }

    [PunRPC]
    void ShootBullet(int userid)
    {
        GameObject tempBullet= Instantiate(_bullet, _bulletOrigin.position, transform.rotation);     //when mouse is pressedspawn a bullet gameobject at custom position and set its velocity and get the bulletholder script to set values in there.
        Rigidbody rb = tempBullet.GetComponent<Rigidbody>();
        BulletHolder bh = tempBullet.GetComponent<BulletHolder>();
        bh.Playerid = userid; 
        rb.velocity = tempBullet.transform.forward * _bulletSpeed * Time.fixedDeltaTime;
    }
}
