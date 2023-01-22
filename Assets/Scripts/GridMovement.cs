using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridMovement : MonoBehaviour
{
    Grid grid;
    public GameObject cube;

    [SerializeField]
    int gridx=10;
    [SerializeField]
    int gridz=10;
    [SerializeField]
    int gridcellsize=1;

    private List<float> distance;
    private float calcDistance;
    private int arrayNumber;

     public List<Vector3> calculatedMovementList;
    private void Awake()
    {
        calculatedMovementList = new List<Vector3>();
        
        grid = new Grid(gridx, gridz, gridcellsize);
    }
    private void Start()
    {
      
        for (int i = 0; i < grid.GetGridPositionList().Count; i++)
        {
            
           GameObject floor= Instantiate(cube, grid.GetGridPositionList()[i], cube.transform.rotation);
            floor.transform.parent = transform;
        }


    }
    public void CalculateMovement(Vector3 current,Vector3 target)
    {
        distance = new List<float>();
        if (current == target)
        {
            return;
        }
        for (int i = 0; i <grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGrid().GetLength(1); j++)
            {
               if(current==grid.GetWorldPosition(i, j))
               {
                    if(i+1<=gridx-1)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance( grid.GetWorldPosition(i + 1, j), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (j + 1 <= gridz - 1)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance(grid.GetWorldPosition(i , j+1), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (i - 1 >= 0)
                    {
                        distance.Add( Mathf.Abs(Vector3.Distance(grid.GetWorldPosition(i - 1, j), target)));
                    }
                    else
                    {
                        distance.Add(Mathf.Infinity);
                    }
                    if (j - 1 >= 0)
                    {
                        distance.Add(Mathf.Abs(Vector3.Distance(grid.GetWorldPosition(i , j-1), target)));
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
                        calculatedMovementList.Add(grid.GetWorldPosition(i + 1, j));
                        CalculateMovement(grid.GetWorldPosition(i + 1, j), target);
                        return;
                    }
                    else if (arrayNumber == 1)
                    {
                        calculatedMovementList.Add(grid.GetWorldPosition(i, j+1));
                        CalculateMovement(grid.GetWorldPosition(i , j+1), target);
                        return;
                    }
                    else if (arrayNumber == 2)
                    {
                        calculatedMovementList.Add(grid.GetWorldPosition(i - 1, j));
                        CalculateMovement(grid.GetWorldPosition(i - 1, j), target);
                        return;
                    }
                    else if (arrayNumber == 3)
                    {
                        calculatedMovementList.Add(grid.GetWorldPosition(i, j-1));
                        CalculateMovement(grid.GetWorldPosition(i , j-1), target);
                        return;
                    }
                   
               }

            }
        }
    }
   
}
