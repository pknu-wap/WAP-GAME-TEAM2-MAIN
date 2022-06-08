using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualEntity : MonoBehaviour
{
    private void Update()
    {
        if (!BuildManager.Instance.IsTouch) return;

        InputEventManager.Instance.AddTouchEvent(MoveToTouch);
    }
    private void MoveToTouch(Touch touch)
    {
        Vector3 offset = FieldManager.Instance.StartPos.position;
        Vector3 position = FieldManager.Instance.GetWorldPosition(touch.position);
        position = FieldManager.Instance.GetGridPosition(position - offset);

        transform.position = position;
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
        Cell cell = other.GetComponent<Cell>();
        if (!BuildManager.Instance.IsTouch && cell != null) {
            if (cell.CellState == Cell.State.BUILDABLE)
            {
                cell.CellState = Cell.State.UNBUILDABLE;
                cell.UpdateCellColor(Cell.State.DEFAULT);
                FieldManager.Instance.AddTowerList(gameObject.GetComponent<Tower>());
                Destroy(this);
            }
            else
            {
                cell.UpdateCellColor(Cell.State.DEFAULT);
                FieldManager.Instance.RemoveTowerList(gameObject.GetComponent<Tower>());
                Destroy(gameObject);
            }

        }
    }
    public void SetTurret()
    {
        Color color = GetComponentInChildren<MeshRenderer>().material.color;
        color.a = 1f;
        GetComponent<Tower>().enabled = true;
        GetComponent<EntityHealth>().enabled = true;
        GetComponent<AttackProjectile>().enabled = true;
        GetComponentInChildren<Collider>().isTrigger = false;
        GetComponentInChildren<MeshRenderer>().material.color = color;

        //값을 임의로 지정하여 테스트 중임
        GetComponent<EntityHealth>().Init(100, 100);
        GetComponent<AttackProjectile>().Init(30, 20, 1, 1);
    }
    public void SetVirtualTurret()
    {
        Color color = GetComponentInChildren<MeshRenderer>().material.color;
        color.a = 0.4f;
        GetComponent<Tower>().enabled = false;
        GetComponent<EntityHealth>().enabled = false;
        GetComponent<AttackProjectile>().enabled = false;
        GetComponentInChildren<Collider>().isTrigger = true;
        GetComponentInChildren<MeshRenderer>().material.color = color;
    }
    private void OnDestroy()
    {
        InputEventManager.Instance.RemoveTouchEvent(MoveToTouch);
    }
}
