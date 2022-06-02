using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject selectNode;
    public GameObject tower;
    [SerializeField] GameObject towerPrefabs;
  
    public bool IsTouch { get; private set; }




    private void Update()
    {
        InputEventManager.Instance.AddTouchEvent(SelectTurret);
    }
    public void BuildToTower(GameObject target)
    {
       Instantiate(tower, selectNode.transform.position, Quaternion.identity);//생성할 오브젝트, 생성될 위치, 각도
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
                    if (hit.collider.tag == "Turret")
                    {
                        tower = Instantiate(towerPrefabs, GetWorldPosition(touch.position), Quaternion.identity);
                        SetTurret(false);
                        
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
                SetTurret(true);
                break;

        }
    }
    private void SetTurret(bool isBuild)
    {
        Color color = tower.GetComponent<MeshRenderer>().material.color;
        if (isBuild)
        {
            color.a = 1f;
            tower.GetComponent<Tower>().enabled = true;
            tower.GetComponent<EntityHealth>().enabled = true;
            tower.GetComponent<AttackProjectile>().enabled = true;
            tower.GetComponent<VirtualEntity>().enabled = false;
            tower.GetComponent<Collider>().isTrigger = false;

            FieldManager.Instance.towerList.Add(tower);
        }
        else
        {
            color.a = 0.4f;
            tower.GetComponent<Tower>().enabled = false;
            tower.GetComponent<EntityHealth>().enabled = false;
            tower.GetComponent<AttackProjectile>().enabled = false;
            tower.GetComponent<VirtualEntity>().enabled = true;
            tower.GetComponent<Collider>().isTrigger = true;
        }
        tower.GetComponent<MeshRenderer>().material.color = color;
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
        Vector3 offset = new Vector3(position.x % cellSize, -2, position.z % cellSize);

        return position - offset;
    }
}
