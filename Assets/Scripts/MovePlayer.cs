using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    float movespeed=10f;
    
    PhotonView myview;
    TurnManager tm;
    GridMovement Gm;
    Coroutine MoveIE;
    bool movementFinished=true;
    private void Awake()
    {
        tm = GameObject.FindObjectOfType<TurnManager>();
        Gm = GameObject.FindObjectOfType<GridMovement>();
        myview = GetComponent<PhotonView>();
        Debug.Log(myview.Owner.ActorNumber);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && movementFinished==true && myview.IsMine && tm.playerTurn==myview.Owner.ActorNumber)
        {
            
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    movementFinished = false;
                    Gm.calculatedMovementList.Clear();
                    Gm.CalculateMovement(transform.position, hit.collider.transform.position);
                    StartCoroutine(MoveTowardsTarget());
                }
                
            }
           
        }
    }
    IEnumerator MoveTowardsTarget()
    {
        for (int i = 0; i < Gm.calculatedMovementList.Count; i++)
        {
           MoveIE= StartCoroutine(Moving(i));
            yield return MoveIE;
            
        }
    }
    IEnumerator Moving(int i)
    {
        while (transform.position!=Gm.calculatedMovementList[i])
        {
            transform.position = Vector3.MoveTowards(transform.position, Gm.calculatedMovementList[i], movespeed * Time.deltaTime);
            yield return null;  
        }
        yield return new WaitForSeconds(0.1f);
        if (i == Gm.calculatedMovementList.Count - 1)
        {
            movementFinished = true;

            tm.setPlayerTurn();
        }
    }
}
