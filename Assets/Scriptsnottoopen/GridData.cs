using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridData : MonoBehaviour     //Holds the Grid Width.height and cell size.
{
    public static GridData current;

    public int gridx=10;

    public int gridz=10;

    public int gridcellsize=1;

    public List<Vector3> calculatedMovementList; //Holds A list of vector 3 values that have been calculated from player position to target position.
    private void Awake()
    {
        current = this;
        calculatedMovementList = new List<Vector3>(); // instantiate the list
    }
   
   
}
