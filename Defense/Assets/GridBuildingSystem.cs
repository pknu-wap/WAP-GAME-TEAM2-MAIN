using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    public GameObject turret;
    private RaycastHit hit;
    private RaycastHit beforehit;
    private GameObject target;
    public GameObject testturret;
    private GameObject testTR;
    private Vector3 turretPos;
    private Vector3 beforeturretPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(ray,out hit)){
                target = hit.transform.gameObject;
            }
        }
        if(Input.GetMouseButton(1) && target != null)
        {
            float targetZ = target.transform.position.z;
            target.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.transform.position = 
        }*/






        if (Input.GetMouseButton(1))//DOWN
        {
            if (Physics.Raycast(ray, out hit))
            {
                turretPos = hit.transform.position + Vector3.up; //반투명 가상터렛 위치
                if (turretPos.y == 0.5)
                {
                    testTR = Instantiate(testturret, turretPos, Quaternion.identity);
                }
                if (turretPos.y == 0.5)//조건 추가
                 {
                     if (beforeturretPos.y != turretPos.y)
                     {
                         Destroy(testTR);
                     }
                 }
                beforeturretPos = turretPos;
                /*if (beforehit.collider.gameObject.GetComponent<Renderer>().transform.position != hit.collider.gameObject.GetComponent<Renderer>().transform.position)
                {
               
                beforehit = hit;*/

            }
        }
        if (Input.GetMouseButtonUp(1))//우클릭 시 터렛 설치
        {
            Destroy(testTR);
            turretPos.y -= 1;
            GameObject TR = Instantiate(turret, turretPos, Quaternion.identity);
        }


        /*if (Input.GetMouseButton(1))
        {

        }*/




    }
}
