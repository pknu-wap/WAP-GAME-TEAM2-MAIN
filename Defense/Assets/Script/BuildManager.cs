using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject selectNode;
    private GameObject tower;
    [SerializeField] GameObject[] towerPrefabs;

    public bool IsTouch { get; private set; } = false;
    public void BuildToTower()
    {
        if (IsTouch)
        {
            IsTouch = false;
            if (tower == null) return;
            tower.GetComponent<VirtualEntity>().SetTower();
            tower = null;
        }
    }

    public void SelectTower(int turretNum)
    {
        //임시 가격
        if (!GameManager.Instance.CheckPurchase(100)) return;
        IsTouch = true;
        Touch touch = Input.GetTouch(0);
        Vector3 offset = FieldManager.Instance.StartPos.position;
        Vector3 towerPosition = FieldManager.Instance.GetWorldPosition(touch.position);
        towerPosition = FieldManager.Instance.GetGridPosition(towerPosition - offset);

        tower = ObjectPoolAdmin.Instance.GetPooledObject(towerPrefabs[turretNum]);
        tower.SetActive(true);
        tower.transform.position = towerPosition;
        tower.GetComponent<VirtualEntity>().SetVirtualTower();
    }
}
