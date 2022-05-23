using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            float m_ZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector2 touchPos = Input.GetTouch(0).position;
            Vector3 pos = new Vector3(touchPos.x, touchPos.y, m_ZCoord);
            transform.position = Camera.main.ScreenToWorldPoint(pos);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Debug.Log(transform.position);
        }
    }
    
    private Vector3 GetWorldPosition(Vector2 position)
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);

        return touchPos;
    }
}
