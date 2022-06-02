using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject selectNode;
    private VirtualEntity tower;
    [SerializeField] VirtualEntity towerPrefabs;

    public bool IsTouch { get; private set; } = false;

    private void Update()
    {
        InputEventManager.Instance.AddTouchEvent(SelectTurret);
    }
    public void BuildToTower(Touch touch)
    {
        tower = Instantiate<VirtualEntity>(towerPrefabs, GetWorldPosition(touch.position), Quaternion.identity);
        tower.SetVirtualTurret();
    }

    private void SelectTurret(Touch touch)
    {
        Ray ray;
        RaycastHit hit;
        switch (touch.phase)
        {
            case TouchPhase.Began:
                IsTouch = true;
                ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    tower = hit.collider.GetComponent<VirtualEntity>();
                    if (tower != null)
                    {
                        if (tower.isFixed) return;
                        BuildToTower(touch);
                    }
                }
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                if (tower == null) return;
                tower.transform.position = GetWorldPosition(touch.position);
                break;
            case TouchPhase.Ended:
                IsTouch = false;
                if (tower == null) return;
                tower.isFixed = true;
                tower.SetTurret();
                tower = null;
                break;

        }
    }
    private Vector3 GetWorldPosition(Vector2 touch)
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
        Vector3 offset = new Vector3(position.x % cellSize, 0, position.z % cellSize);

        return position - offset;
    }
}
