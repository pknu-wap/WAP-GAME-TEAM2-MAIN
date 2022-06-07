using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public int Level { get; private set; }
    public bool IsStart { get; private set; }
    public int Money { get; private set; }

    [SerializeField] EntityBaseInfo[] enemyPrefab;
    [SerializeField] Text remainTimeText;
    [SerializeField] Button readyButton;
    [SerializeField] Text levelText;
    [SerializeField] Text moneyText;
    [SerializeField] ParticleSystem[] particles;

    private const int WAITING_TIME = 120;
    private float cntTime;
    private float remainTime;
    private List<EntityBaseInfo> enemyList;
    void Start()
    {
        IsStart = false;
        Level = 0;
        cntTime = 0;
        enemyList = new List<EntityBaseInfo>();
        UpdateLevelText(Level);
        StartCoroutine(NextPhaseTimer());
    }

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
        UpdateLevelText(++Level);
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
        EntityBaseInfo enemy = Instantiate<EntityBaseInfo>(enemyPrefab[index]);
        if (enemy != null)
        {
            enemyList.Add(enemy);
        }
    }
    public void UseSkill(int index)
    {
        ParticleSystem skill = Instantiate<ParticleSystem>(particles[index], new Vector3(0, 1, 0), Quaternion.identity);
        StartCoroutine(SkillEffect(skill));
    }
    private IEnumerator SkillEffect(ParticleSystem skill)
    {
        skill.Play();
        yield return new WaitUntil(() => skill.isPlaying == false);
        Destroy(skill.gameObject);
    }

    private void UpdateLevelText(int level)
    {
        Level = level;
        levelText.text = "Level\n" + Level;

    }
    public void UpdateMoneyText(int money)
    {
        Money += money;
        moneyText.text = "Money\n" + Money;
    }
}
