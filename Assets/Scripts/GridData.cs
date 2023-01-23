using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GridData : MonoBehaviour
{
    public static GridData current;

    [SerializeField]
    public int gridx=10;
    [SerializeField]
    public int gridz=10;
    [SerializeField]
    public int gridcellsize=1;

     public List<Vector3> calculatedMovementList;
    private void Awake()
    {
        current = this;
        calculatedMovementList = new List<Vector3>();
    }
   
   
}
