using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : MonoSingleton<TestTurret>
{
    private RaycastHit hit;
    private Vector3 turretPos;// 설치 포탑 위치
    private GameObject testTR;//포탑
    public GameObject testturret;//포탑
    private float cellSize;
    // Start is called before the first frame update
    void Start()
    {
        cellSize = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))//DOWN
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.position.y < 0.5)//기본 셀에 대해서만 포탑이 설치되게 함
                {
                    turretPos = hit.transform.position + Vector3.up; // 설치될 포탑 위치 기본셀보다 1칸 위에 설치되므로 Vector.up을 더해줌 
                    testTR = Instantiate(testturret, turretPos, Quaternion.identity);
                    testTR.transform.localScale = new Vector3(cellSize, 1, cellSize);
                }
            }
        }
    }
}
