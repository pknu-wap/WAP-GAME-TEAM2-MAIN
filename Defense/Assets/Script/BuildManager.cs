using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject selectNode;
    private VirtualEntity tower;
    [SerializeField] VirtualEntity[] towerPrefabs;

    public bool IsTouch { get; private set; } = false;
    public void BuildToTower()
    {
        IsTouch = false;
        if (tower == null) return;
        tower.isFixed = true;
        tower.SetTurret();
        FieldManager.Instance.towerList.Add(tower.gameObject);
        tower = null;
    }

    public void SelectTurret(int turretNum)
    {
        IsTouch = true;
        Touch touch = Input.GetTouch(0);
        tower = Instantiate<VirtualEntity>(towerPrefabs[turretNum], GetWorldPosition(touch.position), Quaternion.identity);
        tower.SetVirtualTurret();
    }
    public Vector3 GetWorldPosition(Vector2 touch)
    {
        float m_ZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 touchPos = new Vector3(touch.x, touch.y, m_ZCoord);
  
        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        touchPos.y = 0;
        return GetGridPosition(touchPos);
    }
    private Vector3 GetGridPosition(Vector3 position)
    {
        int cellSize = FieldManager.Instance.cellSize;
        Vector3 offset = new Vector3(position.x % cellSize, -2, position.z % cellSize);

        return position - offset;
    }
}
