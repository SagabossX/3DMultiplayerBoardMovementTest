using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridView : MonoBehaviour
{
    public static GridView current;
    public Grid grid;                                                                                      //reference to grid class 
    public GameObject quad;                                                                               //reference Gameobject to spawn as floor, we are using a quad tagged as Ground with collider.

    private void Awake()
    {
        current = this;
        grid = new Grid(GridData.current.gridx, GridData.current.gridz, GridData.current.gridcellsize);    // Create a new grid from griddata.
    }
    private void Start()
    {
      
        for (int i = 0; i < grid.GetGridPositionList().Count; i++) //loop through the grid
        {
            
           GameObject floor= Instantiate(quad, grid.GetGridPositionList()[i], quad.transform.rotation);    //Instantiate a quad in every cell of grid.
            floor.transform.parent = transform;                                                           //set parent of instantiated object to this gameobject for better sorting.
        }


    }
   
}
