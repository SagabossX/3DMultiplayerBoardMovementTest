using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
public class MovePlayer : MonoBehaviour //sits on the player GameObject.
{
    public static MovePlayer current;
    
    PhotonView myview;

    private void Awake()
    {
        myview = GetComponent<PhotonView>();
        if (myview.IsMine)
        {
            current = this;                   //set instance to this if it is main player.
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && AnimatePlayer.current.movementFinished==true && myview.IsMine && TurnManager.current.playerTurn==myview.Owner.ActorNumber) //check for mouse button,movement finished and current turn
        {
            
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);          //create a ray from camera to mouse position
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Ground"))                                //if we hit object with tag ground that we have instantiated in grid view script.
                {
                    AnimatePlayer.current.movementFinished = false;                                         //st movement finished to false so this doesnt run again until player has finished moving.
                    GridData.current.calculatedMovementList.Clear();                  //Clear previus movement data.
                    GridController.current.CalculateMovement(transform.position, hit.collider.transform.position); //Calculate new movement data
                    AnimatePlayer.current.MoveGameobjectInGrid(GridData.current.calculatedMovementList,transform);
                }
                
            }
           
        }
    }
}
