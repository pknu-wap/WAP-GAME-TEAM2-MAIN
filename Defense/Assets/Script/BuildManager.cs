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
        Vector3 towerPosition = FieldManager.Instance.GetWorldPosition(touch.position);
        tower = Instantiate<VirtualEntity>(towerPrefabs[turretNum],towerPosition, Quaternion.identity);
        tower.SetVirtualTurret();
    }
}
