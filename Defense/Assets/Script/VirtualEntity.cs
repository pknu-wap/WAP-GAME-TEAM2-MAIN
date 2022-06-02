using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualEntity : MonoBehaviour
{
    private Color cellOrigin;
    enum CellState
    {
        BUILDABLE,UNBUILDABLE,EXIT
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;

        CheckBuildable(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;

        Cell cell = other.GetComponent<Cell>();
        if (cell != null) ChangeState(cell.gameObject, CellState.EXIT);
    }
    private void CheckBuildable(Collider hit)
    {
        Cell cell = hit.GetComponent<Cell>();
        if (cell != null)
        {
            cellOrigin = cell.GetComponent<MeshRenderer>().material.color;
            if (cell.buildable) ChangeState(cell.gameObject, CellState.BUILDABLE);
            else ChangeState(cell.gameObject,CellState.UNBUILDABLE);
        }
    }
    private void ChangeState(GameObject target, CellState state)
    {
        Color color = target.GetComponent<MeshRenderer>().material.color;
        switch (state)
        {
            case CellState.BUILDABLE:
                color = Color.yellow;
                color.a = 0.4f;
                break;
            case CellState.UNBUILDABLE:
                color = Color.red;
                color.a = 0.4f;
                break;
            case CellState.EXIT:
                color = cellOrigin;
                break;
        }
        target.GetComponent<MeshRenderer>().material.color = color;
    }
}
