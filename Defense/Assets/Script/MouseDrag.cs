using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoSingleton<MouseDrag>
{
    private Vector3 m_Offset;
    private float m_ZCoord;
    RaycastHit hit;// 광선에 맞은 오브젝트
   
    void OnMouseDown()
    {
        m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; 
        m_Offset = gameObject.transform.position - GetMouseWorldPosition(); // - new Vector3(0, mousePos.y, 0);
    }
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + m_Offset;
        transform.position += Vector3.up*10;// 카메라의 각도가 y축에 대해 수평이 아니라서 드래그할 때 y축의 값이 움직인다. 따라서 10을 곱해 큰값으로 만들어주어 필드 위에 있게 만들어 준다.
    }
    void OnMouseUp()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            if (hit.transform.position.y < 0.5)
                transform.position = hit.collider.transform.position + new Vector3(0, 1, 0);//셀 위에 배치되게 함
            else
                transform.position = hit.collider.transform.position + new Vector3(4, 0, 0);//레드존 블루존에 드래그 했을 때 오른쪽 셀에 자동배치되게함
        }
    }
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}