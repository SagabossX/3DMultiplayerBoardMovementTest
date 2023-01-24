using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridController : MonoBehaviour
{
    public static GridController current;

    private List<float> _distance;
    private float _calcDistance;
    private int _arrayNumber;
    private void Awake()
    {
        current = this;
    }
    public void CalculateMovement(Vector3 current,Vector3 target)                           // get current grid position from player and target grid position to go to.
    {
        _distance = new List<float>();                 //instantiate a new distance list each time the function is called.
        if (current == target)                         // we have reached the destination.
        {
            return;
        }
        for (int i = 0; i <GridView.current.grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < GridView.current.grid.GetGrid().GetLength(1); j++)                   // iterate through the 2d array/grid.
            {
               if(current== GridView.current.grid.GetWorldPosition(i, j))                  //find the current position in the grid.
               {

                    //Below we check the distance from the top bottom right and left cell to the target.
                    if(i+1<=GridData.current.gridx-1)    //check if surrounding cells are inside the grid.
                    {
                        _distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i + 1, j), target))); //add distance from surrounding cell to the target to the _distance list.
                    }
                    else
                    {
                        _distance.Add(Mathf.Infinity); // add infinity to distance if it is outside the grid.
                    }
                    if (j + 1 <= GridData.current.gridz - 1)
                    {
                       _distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i , j+1), target)));
                    }
                    else
                    {
                        _distance.Add(Mathf.Infinity);
                    }
                    if (i - 1 >= 0)
                    {
                       _distance.Add( Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i - 1, j), target)));  //(i,j) is our current cell i+1,i-1 returns the cell above and below,  j+1,j-1 return riight and left cells.
                    }
                    else
                    {
                        _distance.Add(Mathf.Infinity);
                    }
                    if (j - 1 >= 0)
                    {
                        _distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i , j-1), target)));
                    }
                    else
                    {
                        _distance.Add(Mathf.Infinity);
                    }
                    //In the Above lines we have added 4 values to the distance list 

                    _calcDistance = _distance[0];                                    //set the calcdistance value to the first element. 
                    _arrayNumber = 0;            

                    for (int x = 0; x < _distance.Count; x++)                      
                    {
                        if (_distance[x] < _calcDistance)                           //Calcualte the shortest ditance to target and set arraynumber to the shortest distance so we know wich path to take next.
                        {
                            _calcDistance = _distance[x];
                            _arrayNumber = x;
                        }
                    }

                    //Below we check which cell returned the shortest path, that is indicated by array number, and we add the cell to the calculatedmovement list.
                    //Call the CalculateMovement function until we have reached the destination.
                    if (_arrayNumber == 0)                                          
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i + 1, j));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i + 1, j), target);     //insert new current values based on the shortest distance and inherit the target value.
                        return;
                    }
                    else if (_arrayNumber == 1)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i, j+1));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i , j+1), target);
                        return;
                    }
                    else if (_arrayNumber == 2)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i - 1, j));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i - 1, j), target);
                        return;
                    }
                    else if (_arrayNumber == 3)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i, j-1));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i , j-1), target);
                        return;
                    }
                   
               }

            }
        }
    }
   
}
