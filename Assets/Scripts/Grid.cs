using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private int cellSize;
    private int[,] grid;
    List<Vector3> gridPositionList;
    public Grid(int width,int height,int cellSize) //Constructor to initialise the values 
    {

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        grid = new int[width, height];
        gridPositionList = new List<Vector3>();

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                gridPositionList.Add(GetWorldPosition(i,j));
             
            }
        }
        
    }
    public Vector3 GetWorldPosition(int x,int z)
    {
        return new Vector3(x,0,z) * cellSize;     // return worldpostion of cells.
    }
    public List<Vector3> GetGridPositionList()
    {
        return gridPositionList;       //return a list of worldpostions of cells.
    }
    public int[,] GetGrid()
    {
        return grid;                 // return grid object
    }
}
