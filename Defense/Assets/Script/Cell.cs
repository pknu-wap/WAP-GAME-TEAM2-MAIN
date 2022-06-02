using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Color cellOrigin;
    private MeshRenderer mesh;
    private VirtualEntity turret;
    public enum State
    {
        DEFAULT,BUILDABLE, UNBUILDABLE, INSTALLED
    }
    public State CellState;
    private void Start()
    {
        CellState = State.BUILDABLE;
        mesh = GetComponent<MeshRenderer>();
        cellOrigin = mesh.material.color;
    }
    public void UpdateCellColor(State state)
    {
        Color color = GetComponent<MeshRenderer>().material.color;
        switch (state)
        {
            case State.BUILDABLE:
                color = Color.yellow;
                color.a = 0.4f;
                break;
            case State.UNBUILDABLE:
                color = Color.red;
                color.a = 0.4f;
                break;
            case State.INSTALLED:
                color = cellOrigin;
                break;
            case State.DEFAULT:
                color = cellOrigin;
                break;
        }
        GetComponent<MeshRenderer>().material.color = color;
    }
}
