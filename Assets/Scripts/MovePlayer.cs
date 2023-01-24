using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
public class MovePlayer : MonoBehaviour //sits on the player GameObject.
{
    public static MovePlayer current;
    [SerializeField]
    float _movespeed=10f;
    
    PhotonView myview;
    Coroutine MoveIE;
    bool _movementFinished=true;

    public event Action OnPlayerFinishedMove;  //Call this when current player finishes the move

    public void PlayerFinishedMove()
    {
        if (OnPlayerFinishedMove != null) 
        {
            OnPlayerFinishedMove();
        }
    }
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
       
        if (Input.GetMouseButtonDown(0) && _movementFinished==true && myview.IsMine && TurnManager.current.playerTurn==myview.Owner.ActorNumber) //check for mouse button,movement finished and current turn
        {
            
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);          //create a ray from camera to mouse position
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Ground"))                                //if we hit object with tag ground that we have instantiated in grid view script.
                {
                    _movementFinished = false;                                         //st movement finished to false so this doesnt run again until player has finished moving.
                    GridData.current.calculatedMovementList.Clear();                  //Clear previus movement data.
                    GridController.current.CalculateMovement(transform.position, hit.collider.transform.position); //Calculate new movement data
                    StartCoroutine(MoveTowardsTarget());
                }
                
            }
           
        }
    }
    IEnumerator MoveTowardsTarget()  
    {
        for (int i = 0; i < GridData.current.calculatedMovementList.Count; i++)//for each element in the list of vector 3 start coroutine moving.
        {
           MoveIE= StartCoroutine(Moving(i));         //store the courotine so we can wait till it finishes
            yield return MoveIE;                      //Wait until Moving Finishes to go to the next cycle
            
        }
    }
    IEnumerator Moving(int i)
    {
        while (transform.position!=GridData.current.calculatedMovementList[i])
        {
            transform.position = Vector3.MoveTowards(transform.position, GridData.current.calculatedMovementList[i], _movespeed * Time.deltaTime); // move from current to the vector3 element 
            yield return null;  
        }
        yield return new WaitForSeconds(0.1f);                       //Wait 0.1 seconds after each cell move to make it feel board gamish.
        if (i == GridData.current.calculatedMovementList.Count - 1) // if the last move is dones
        {
            _movementFinished = true;                              
            OnPlayerFinishedMove();                               //Fire the action for turnmanagers listener.
        }
    }
}
