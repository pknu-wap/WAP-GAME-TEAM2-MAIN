using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoSingleton<FieldManager>
{
    public List<GameObject> towerList = new List<GameObject>();

    [SerializeField] GameObject cells;//기본 셀 프리팹
    [SerializeField] GameObject blueZones;//블루존 프리팹
    [SerializeField] GameObject redZones;//레드존 프리팹

    [SerializeField] Transform startPos; // 필드 시작 포지션
    [SerializeField] Transform endPos; // 필드 끝 포지션
    //필드의 크기를 얻기위해 두 포지션을 씬에 배치해놨음

    public int[,] gridArray;
    public int cellSize;

    private const int MAXBLUE = 3;
    private const int MAXRED = 3;

    private int width;
    private int height;


    private int cntBlue = 0;
    private int cntRed = 0;

    void Start()
    {
        width = (int)(endPos.position.x - startPos.position.x);
        height = (int)(endPos.position.z - startPos.position.z);
        cellSize = 4;
        gridArray = new int[width, height];
        CreateField(cellSize);
    }
    private void CreateField(int cellSize)
    {
        for (int x = 0; x < gridArray.GetLength(0) / cellSize; x++)
        {
            for (int z = 0; z < gridArray.GetLength(1) / cellSize; z++)
            {
                Vector3 position = GetWorldPosition(x, z);
                CreateCell(position);
            }
        }
    }
    private void CreateCell(Vector3 position)
    {
        if ((position.x > startPos.position.x && position.x < endPos.position.x)
            || (position.z > startPos.position.z && position.z < startPos.position.z))
        {
            //*****if문에 조건을 추가해 맵 중앙 지켜야될 구조물에 겹쳐서 생기지 않게 한다.

            int random = Random.Range(0, 300);//1~300범위의 랜덤수 
            if ((90 < random && random <= 95) && cntBlue < MAXBLUE)//랜덤수가 90~95이면 블루존
            {
                GameObject blueZone = Instantiate(blueZones, position + Vector3.up, Quaternion.identity);

                blueZone.transform.localScale = new Vector3(cellSize, 1, cellSize); // cell Size만큼 크기를 키운다
                cntBlue++;
                return; // 해당 함수 종료
            }
            else if ((95 < random && random <= 100) && cntRed < MAXRED)//랜덤수가 96~100이면 레드존
            {
                GameObject redZone = Instantiate(redZones, position + Vector3.up, Quaternion.identity);
                redZone.transform.localScale = new Vector3(cellSize, 1, cellSize);
                cntRed++;
                return;
            }
        }
        GameObject cell = Instantiate(cells, position + Vector3.up, Quaternion.identity);//기본 그리드
        cell.transform.localScale = new Vector3(cellSize, 1, cellSize);
        DrawLine(position);
    }
    private void DrawLine(Vector3 position)
    {
        //직사각형 모양으로 만들어주는 것
        Debug.DrawLine(new Vector3(position.x, 0, position.z), new Vector3(position.x + cellSize, 0, position.z), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x, 0, position.z), new Vector3(position.x, 0, position.z + cellSize), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x + cellSize, 0, position.z), new Vector3(position.x + cellSize, 0, position.z + cellSize), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x, 0, position.z + cellSize), new Vector3(position.x + cellSize, 0, position.z + cellSize), Color.yellow, 100f);
    }
    private Vector3 GetWorldPosition(int x, int z)
    {
        return startPos.position + new Vector3(x, 0, z) * cellSize;
    }
}
