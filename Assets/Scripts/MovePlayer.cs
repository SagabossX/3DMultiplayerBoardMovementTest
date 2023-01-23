using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
public class MovePlayer : MonoBehaviour
{
    public static MovePlayer current;
    [SerializeField]
    float movespeed=10f;
    
    PhotonView myview;
    Coroutine MoveIE;
    bool movementFinished=true;

    public event Action OnPlayerFinishedMove;

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
            current = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && movementFinished==true && myview.IsMine && TurnManager.current.playerTurn==myview.Owner.ActorNumber)
        {
            
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    movementFinished = false;
                    GridData.current.calculatedMovementList.Clear();
                    GridController.current.CalculateMovement(transform.position, hit.collider.transform.position);
                    StartCoroutine(MoveTowardsTarget());
                }
                
            }
           
        }
    }
    IEnumerator MoveTowardsTarget()
    {
        for (int i = 0; i < GridData.current.calculatedMovementList.Count; i++)
        {
           MoveIE= StartCoroutine(Moving(i));
            yield return MoveIE;
            
        }
    }
    IEnumerator Moving(int i)
    {
        while (transform.position!=GridData.current.calculatedMovementList[i])
        {
            transform.position = Vector3.MoveTowards(transform.position, GridData.current.calculatedMovementList[i], movespeed * Time.deltaTime);
            yield return null;  
        }
        yield return new WaitForSeconds(0.1f);
        if (i == GridData.current.calculatedMovementList.Count - 1)
        {
            movementFinished = true;
            OnPlayerFinishedMove();
        }
    }
}
