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
    [SerializeField] Button skillButton;
    [SerializeField] Text levelText;
    [SerializeField] Text moneyText;
    [SerializeField] Image buildPanel;
    [SerializeField] APlayerSkill[] skills;
    [SerializeField] ParticleSystem touchEffect;
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
        buildPanel.gameObject.SetActive(false);
        skillButton.gameObject.SetActive(true);
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
        buildPanel.gameObject.SetActive(true);
        skillButton.gameObject.SetActive(false);
    }
    private IEnumerator NextPhaseTimer()
    {
        cntTime = 0f;
        remainTime = 0f;
        while(cntTime <= WAITING_TIME && !IsStart)
        {
            cntTime = Time.time;
            remainTime = (int)(WAITING_TIME - cntTime);
            remainTimeText.text = remainTime+ "";
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
        APlayerSkill skill = Instantiate<APlayerSkill>(skills[index]);
        skill.Use();
        StartCoroutine(skill.DelayTime(skillButton));
    }
    public IEnumerator ParticleEffect(ParticleSystem particle, Vector3 position)
    {
        ParticleSystem effect = Instantiate<ParticleSystem>(particle, position, Quaternion.identity);
        effect.Play();
        yield return new WaitUntil(() => effect.isPlaying == false);
        Destroy(effect.gameObject);
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
