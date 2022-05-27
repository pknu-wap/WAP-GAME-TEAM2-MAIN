using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject selectNode;
    public GameObject tower;

    [SerializeField] Material buildable_Cell;
    [SerializeField] Material notBuildable_Cell;
    [SerializeField] Material default_Cell;



    public void BuildToTower()
    {
       Instantiate(tower, selectNode.transform.position, Quaternion.identity);//생성할 오브젝트, 생성될 위치, 각도
    }
    private void Start()
    {
    }
    
    private void FixedUpdate()
    {
        InputEventManager.Instance.AddTouchEvent(SelectTurret);
    }
    private void SelectTurret(Touch touch)
    {
        Ray ray;
        RaycastHit hit;
        switch (touch.phase)
        {
            case TouchPhase.Began:
                ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, 100f)){
                    if (hit.collider.tag != "Turret") return;
                    tower = hit.collider.gameObject;
                    tower.transform.position = GetGridPosition(tower.transform.position);
                }
                break;
            case TouchPhase.Moved:
                tower.transform.position = GetWorldPosition(touch.position);
                CheckBuildable(tower.transform);
                break;

        }
    }
    private void CheckBuildable(Transform target)
    {
        RaycastHit hit;
        if(Physics.Raycast(target.position, target.TransformDirection(Vector3.down), out hit))
        {
            if(hit.collider.tag == "Grid")
            {
                Debug.Log("충돌");
                hit.collider.GetComponent<MeshRenderer>().material = buildable_Cell;
                //Color color = hit.collider.GetComponent<MeshRenderer>().material.color;
                //color = Color.yellow;
                //color.a = 0.3f;
                //hit.collider.GetComponent<MeshRenderer>().material.color = color;
            }
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
