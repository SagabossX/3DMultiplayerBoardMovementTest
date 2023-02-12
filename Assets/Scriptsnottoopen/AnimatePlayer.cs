using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatePlayer : MonoBehaviour
{
    public static AnimatePlayer current;
    public bool movementFinished=true;
    private float _movespeed = 10f;
    Coroutine MoveIE;

    public event Action OnPlayerFinishedMove;  //Call this when current player finishes the move
    private void Awake()
    {
        current = this;
    }
    public void PlayerFinishedMove()
    {
        if (OnPlayerFinishedMove != null)
        {
            OnPlayerFinishedMove();
        }
    }
    public void MoveGameobjectInGrid(List<Vector3> movementList,Transform transformToMove)
    {
        StartCoroutine(MoveTowardsTarget(movementList,transformToMove));
    }
    IEnumerator MoveTowardsTarget(List<Vector3> movementList, Transform transformToMove)
    {
        for (int i = 0; i < movementList.Count; i++)//for each element in the list of vector 3 start coroutine moving.
        {
            MoveIE = StartCoroutine(Moving(i,movementList,transformToMove));         //store the courotine so we can wait till it finishes
            yield return MoveIE;                      //Wait until Moving Finishes to go to the next cycle

        }
    }
    IEnumerator Moving(int i,List<Vector3> movementList, Transform transformToMove)
    {
        while (transform.position != movementList[i])
        {
            transformToMove.position = Vector3.MoveTowards(transformToMove.position, movementList[i], _movespeed * Time.deltaTime); // move from current to the vector3 element 
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);                       //Wait 0.1 seconds after each cell move to make it feel board gamish.
        if (i == movementList.Count - 1) // if the last move is dones
        {
            movementFinished = true;
            OnPlayerFinishedMove();                               //Fire the action for turnmanagers listener.
        }
    }
}
