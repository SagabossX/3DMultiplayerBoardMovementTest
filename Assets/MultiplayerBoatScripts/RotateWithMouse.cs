using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RotateWithMouse : MonoBehaviour
{
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
         if (view.IsMine && Time.timeScale==1) //if it is our view and not paused
         {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                transform.LookAt(new Vector3(hit.point.x,2,hit.point.z));        //raycast a ray to ground and make the player turret look at x,z position of hit point.
            }
         }
    }

}
