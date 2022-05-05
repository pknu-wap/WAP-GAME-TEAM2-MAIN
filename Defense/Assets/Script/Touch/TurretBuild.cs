using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuild : MonoBehaviour
{
    private GameObject target;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,100f))
            {
                if (touch.phase == TouchPhase.Began)
                {
                   if(hit.collider.GetComponent<EntityBaseInfo>() != null)
                    {
                    }
                }

            }
        }
    }
}
