using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridView : MonoBehaviour
{
    public static GridView current;
    public Grid grid;
    public GameObject quad;

    private void Awake()
    {
        current = this;
        grid = new Grid(GridData.current.gridx, GridData.current.gridz, GridData.current.gridcellsize);
    }
    private void Start()
    {
      
        for (int i = 0; i < grid.GetGridPositionList().Count; i++)
        {
            
           GameObject floor= Instantiate(quad, grid.GetGridPositionList()[i], quad.transform.rotation);
            floor.transform.parent = transform;
        }


    }
   
}
