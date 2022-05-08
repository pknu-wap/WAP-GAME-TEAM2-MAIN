using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject obj;//기본 그리드
    //public GameObject redZone;//레드존 그리드
    //public GameObject blueZone;//블루존 그리드
    private const int MAXBLUE = 3;
    private const int MAXRED = 3;

    private int cntBlue = 0;
    private int cntRed = 0;
    // Start is called before the first frame update
    void Start()
    {
        GridClass grid = new GridClass(10, 10, 1f);
        for (int x = 0 + 1; x < 10 + 2; x++)
        {
            for (int z = 0 + 1; z < 10 + 2; z++)
            {
                Vector3 position = new Vector3(x - 1, -0.5f, z - 1);
                CreateCell(position);
            }
        }
        
    }
    private void CreateCell(Vector3 position)
    {
        GameObject cell = Instantiate(obj, position, Quaternion.identity);//기본 그리드
        if ((position.x > 0 && position.x < 10) && (position.z > 0 && position.z < 10))// 맵 외곽을 제외하고 블루존 레드존 설치, 10은 맵의 크기에 따라 변해야함(맵 우측(x), 상단(z)의 최고값)
        {//*****if문에 조건을 추가해 맵 중앙 지켜야될 구조물에 겹쳐서 생기지 않게 한다.
            int random = Random.Range(0, 101);//1~100범위의 랜덤수 
            if ((90 < random && random <= 95) && cntBlue < MAXBLUE)//랜덤수가 90~95이면 블루존
            {
                cell.GetComponent<Renderer>().material.color = Color.blue;
                cntBlue++;
            }
            else if ((95 < random && random <= 100) && cntRed < MAXRED)//랜덤수가 96~100이면 레드존
            {
                cell.GetComponent<Renderer>().material.color = Color.red;
                cntRed++;
            }
        }
    }
   /*
    private void CreateCell(Vector3 position)
    {
        GameObject cell = Instantiate(obj, position, Quaternion.identity);//기본 그리드
        if ((position.x > 0 && position.x < 10) && (position.z > 0 && position.z < 10))// 맵 외곽을 제외하고 블루존 레드존 설치, 10은 맵의 크기에 따라 변해야함(맵 우측(x), 상단(z)의 최고값)
        {
            int random = Random.Range(0, 201);//1~100범위의 랜덤수 
            if ((90 < random && random <= 95) && cntBlue < MAXBLUE)//랜덤수가 90~95이면 블루존
            {
                Instantiate(blueZone, position, Quaternion.identity);// 블루존 설치
                cntBlue++;
            }
            else if ((95 < random && random <= 100) && cntRed < MAXRED)//랜덤수가 96~100이면 레드존
            {
                Instantiate(redZone, position, Quaternion.identity);//레드존 설치
                cntRed++;
            }
            else //위 두 범위에 속하지 않으면 기본 그리드 확률적으로 빈도수 높게 설치 됨->레드존 블루존이 맵 전체에 고르게 분포되게 해줌
            {
                Instantiate(obj, position, Quaternion.identity);//기본 그리드
            }
        }
        else
            Instantiate(obj, position, Quaternion.identity);//기본 그리드
    }*/
}

