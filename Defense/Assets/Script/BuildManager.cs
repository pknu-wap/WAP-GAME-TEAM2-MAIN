using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    public GameObject SelectNode;
    public GameObject Tower;
    public GameObject target;

    public void BuildToTower()
    {
       Instantiate(Tower, SelectNode.transform.position, Quaternion.identity);//생성할 오브젝트, 생성될 위치, 각도
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
                    target = hit.collider.gameObject;
                    target.transform.position = GetGridPosition(target.transform.position);
                }
                break;
            case TouchPhase.Moved:
                target.transform.position = GetWorldPosition(touch.position);
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
       //int cellSize = FieldManager.Instance.cellSize;
       /* FieldManger 인스턴스 참조 에러
        * 임시로 CellSize = 4로 진행 */
        Vector3 offset = new Vector3(position.x % 4, 0, position.z % 4);

        return position - offset;
    }
}
