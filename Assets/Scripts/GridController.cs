using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridController : MonoBehaviour
{
    public static GridController current;

    private List<float> distance;
    private float calcDistance;
    private int arrayNumber;
    public List<Vector3> calculatedMovementList;
    private void Awake()
    {
        current = this;
    }
    public void CalculateMovement(Vector3 current,Vector3 target)
    {
        distance = new List<float>();
        if (current == target)
        {
            return;
        }
        for (int i = 0; i <GridView.current.grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < GridView.current.grid.GetGrid().GetLength(1); j++)
            {
               if(current== GridView.current.grid.GetWorldPosition(i, j))
               {
                    if(i+1<=GridData.current.gridx-1)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i + 1, j), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (j + 1 <= GridData.current.gridz - 1)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i , j+1), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (i - 1 >= 0)
                    {
                        distance.Add( Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i - 1, j), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (j - 1 >= 0)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance(GridView.current.grid.GetWorldPosition(i , j-1), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    calcDistance = distance[0];
                    arrayNumber = 0;
                    for (int x = 0; x < distance.Count; x++)
                    {
                        if (distance[x] < calcDistance)
                        {
                            calcDistance = distance[x];
                            arrayNumber = x;
                        }
                    }
                    if (arrayNumber == 0)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i + 1, j));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i + 1, j), target);
                        return;
                    }
                    else if (arrayNumber == 1)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i, j+1));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i , j+1), target);
                        return;
                    }
                    else if (arrayNumber == 2)
                    {
                        GridData.current.calculatedMovementList.Add(GridView.current.grid.GetWorldPosition(i - 1, j));
                        CalculateMovement(GridView.current.grid.GetWorldPosition(i - 1, j), target);
                        return;
                    }
                    else if (arrayNumber == 3)
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
