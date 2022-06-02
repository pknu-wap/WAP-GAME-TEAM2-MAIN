using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualEntity : MonoBehaviour
{
    private MeshRenderer mesh;
    public bool isFixed;
    private void Start()
    {
        isFixed = false;
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;
        Cell cell = other.GetComponent<Cell>();
        if (cell != null)
        {
            switch (cell.CellState)
            {
                case Cell.State.BUILDABLE:
                    cell.UpdateCellColor(Cell.State.BUILDABLE);
                    break;
                case Cell.State.UNBUILDABLE:
                    cell.UpdateCellColor(Cell.State.UNBUILDABLE);
                    break;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;
        Cell cell = other.GetComponent<Cell>();
        if (cell != null)
        {
            cell.UpdateCellColor(Cell.State.DEFAULT);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isFixed) return;

        Cell cell = other.GetComponent<Cell>();
        if (cell != null) {
            if(cell.CellState == Cell.State.BUILDABLE)
            {
                cell.CellState = Cell.State.UNBUILDABLE;
                Destroy(this);
                FieldManager.Instance.towerList.Add(gameObject);
            }
            else Destroy(gameObject);
        cell.UpdateCellColor(Cell.State.DEFAULT);
        }
    }
    public void SetTurret()
    {
        Color color = GetComponent<MeshRenderer>().material.color;
        color.a = 1f;
        //tower.GetComponent<Tower>().enabled = true;
        //tower.GetComponent<EntityHealth>().enabled = true;
        //tower.GetComponent<AttackProjectile>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<MeshRenderer>().material.color = color;
    }
    public void SetVirtualTurret()
    {
        Color color = GetComponent<MeshRenderer>().material.color;
        color.a = 0.4f;
        //tower.GetComponent<Tower>().enabled = false;
        //tower.GetComponent<EntityHealth>().enabled = false;
        //tower.GetComponent<AttackProjectile>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<MeshRenderer>().material.color = color;
    }
}
