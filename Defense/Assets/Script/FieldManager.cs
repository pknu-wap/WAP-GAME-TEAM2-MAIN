using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoSingleton<FieldManager>
{
    public List<Tower> towerList { get; private set; } = new List<Tower>();
    public Transform StartPos;

    [SerializeField] GameObject cells;//기본 셀 프리팹
    [SerializeField] GameObject blueZones;//블루존 프리팹
    [SerializeField] GameObject redZones;//레드존 프리팹

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
        width = (int)(endPos.position.x - StartPos.position.x);
        height = (int)(endPos.position.z - StartPos.position.z);
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
                Vector3 position = new Vector3(x, 0, z) * cellSize;
                CreateCell(FieldManager.Instance.GetGridPosition(position));
            }
        }
    }
    private void CreateCell(Vector3 position)
    {
        if ((position.x > StartPos.position.x && position.x < endPos.position.x)
            || (position.z > StartPos.position.z && position.z < StartPos.position.z))
        {
            //*****if문에 조건을 추가해 맵 중앙 지켜야될 구조물에 겹쳐서 생기지 않게 한다.

            int random = Random.Range(0, 300);//1~300범위의 랜덤수 
            Vector3 pos = new Vector3(position.x, cellSize, position.z);
            if ((90 < random && random <= 95) && cntBlue < MAXBLUE)
            {
                GameObject blueZone = Instantiate(blueZones, pos, Quaternion.identity);
                blueZone.transform.localScale *= cellSize;
                cntBlue++;
                return;
            }
            else if ((95 < random && random <= 100) && cntRed < MAXRED)
            {
                GameObject redZone = Instantiate(redZones, pos, Quaternion.identity);
                redZone.transform.localScale *= cellSize;
                cntRed++;
                return;
            }
        GameObject cell = Instantiate(cells, pos, Quaternion.identity) ;//기본 그리드
        cell.transform.localScale *= cellSize ;
        DrawLine(position);
        }
    }
    private void DrawLine(Vector3 position)
    {
        //직사각형 모양으로 만들어주는 것
        Debug.DrawLine(new Vector3(position.x, 0, position.z), new Vector3(position.x + cellSize, 0, position.z), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x, 0, position.z), new Vector3(position.x, 0, position.z + cellSize), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x + cellSize, 0, position.z), new Vector3(position.x + cellSize, 0, position.z + cellSize), Color.yellow, 100f);
        Debug.DrawLine(new Vector3(position.x, 0, position.z + cellSize), new Vector3(position.x + cellSize, 0, position.z + cellSize), Color.yellow, 100f);
    }
    public Vector3 GetWorldPosition(Vector2 touch)
    {
        float m_ZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 touchPos = new Vector3(touch.x, touch.y, m_ZCoord);

        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        touchPos.y = 0;
        return touchPos;
    }
    public Vector3 GetGridPosition(Vector3 position)
    {
        int gridX = (int)(position.x / cellSize) * cellSize;
        int gridZ = (int)(position.z / cellSize) * cellSize;

        return new Vector3(gridX, 1, gridZ) + StartPos.position;
    }
    public void AddTowerList(Tower tower)
    {
        if (towerList != null && towerList.Contains(tower)) return;
        towerList.Add(tower);
    }
}
