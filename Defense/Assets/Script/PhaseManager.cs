using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoSingleton<PhaseManager>
{
    public int Level { get; private set; }
    public bool IsStart { get; private set; }

    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] Text remainTimeText;
    [SerializeField] Button readyButton;

    private const int WAITING_TIME = 10;
    private float cntTime;
    private float remainTime;
    private List<GameObject> enemyList;
    void Start()
    {
        IsStart = false;
        Level = 0;
        cntTime = 0;
        enemyList = new List<GameObject>();
        StartCoroutine(NextPhaseTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (cntTime >= WAITING_TIME && !IsStart) StartPhase();
        if (enemyList.Count <= 0 && IsStart) EndPhase();
    }

    public void StartPhase()
    {
        IsStart = true;
        remainTimeText.enabled = false;
        readyButton.gameObject.SetActive(false);
        Level++;
        int enemyNum = Mathf.RoundToInt(Level * 3.5f);

        for (int i = 0; i < enemyNum; i++)
        {
            SpawnEnemy();
        }
    }
    private void EndPhase()
    {
        StartCoroutine(NextPhaseTimer());
        IsStart = false;
        remainTimeText.enabled = true;
        readyButton.gameObject.SetActive(true);
    }
    private IEnumerator NextPhaseTimer()
    {
        cntTime = 0f;
        remainTime = 0f;
        while(cntTime <= WAITING_TIME && !IsStart)
        {
            cntTime = Time.time;
            remainTime = (int)(WAITING_TIME - cntTime);
            remainTimeText.text = "다음 페이즈 : " + remainTime;
            yield return null;
        }
    }


    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefab.Length);
        GameObject enemy = Instantiate(enemyPrefab[index]);
        enemyList.Add(enemy);
    }
}
