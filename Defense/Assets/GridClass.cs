using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClass
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    public GameObject obj;
    public GridClass(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                //UtillsClass.CreateWorldText(gridArray[x, y].toString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.yellow, 100f);//100f?
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.yellow, 100f);
                //Instantiate(obj, new Vector3(x, -0.5f, z), Quaternion.identity);
                //x, -0.5(y), z
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.yellow, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.yellow, 100f);
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }
}
