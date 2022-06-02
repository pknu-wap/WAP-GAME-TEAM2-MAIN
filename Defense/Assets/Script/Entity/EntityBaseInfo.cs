using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Entity Info", menuName = "ScriptableObjects/EntityInfo")]
public class EntityBaseInfo : ScriptableObject
{
    [SerializeField]
    private GameObject entityPrefab;
    [Space]
    [SerializeField]
    private double damage;
    [SerializeField]
    private double range;
    [SerializeField]
    private float postAttackDelay;
    [SerializeField]
    private float preAttackDelay;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private int healthPoint;
    [Space]
    [SerializeField]
    private EntityUpgradeInfo upgradeInfo;
    

    public GameObject EntityPrefab { get => entityPrefab; }
    public double Damage { get => damage; }
    public double Range { get => range;  }
    public float PostAttackDelay { get => postAttackDelay; }
    public float PreAttackDelay { get => preAttackDelay;  }
    public float MovementSpeed { get => movementSpeed; }
    public int HealthPoint { get => healthPoint;  }
    public EntityUpgradeInfo UpgradeInfo { get => upgradeInfo; }
}

[Serializable]
public class EntityUpgradeInfo : ScriptableObject
{
    [SerializeField]
    private List<Mesh> meshListPerGrade;
    [SerializeField]
    private List<int> damageMultiplierPerGrade;
    [SerializeField]
    private List<int> healthMultiplierPerGrade;
    [SerializeField]
    private List<int> sizeMultiplierPerGrade;
    [SerializeField]
    private List<int> speedMultiplierPerGrade;
    [SerializeField]
    private List<int> postAttackDelayMultiplierPerGrade;
    [SerializeField]
    private List<int> preAttackDelayMultiplierPerGrade;

    public IReadOnlyList<int> PreAttackDelayMultiplierPerGrade { get => preAttackDelayMultiplierPerGrade; }
    public IReadOnlyList<Mesh> MeshListPerGrade { get => meshListPerGrade; }
    public IReadOnlyList<int> DamageMultiplierPerGrade { get => damageMultiplierPerGrade; }
    public IReadOnlyList<int> HealthMultiplierPerGrade { get => healthMultiplierPerGrade; }
    public IReadOnlyList<int> SizeMultiplierPerGrade { get => sizeMultiplierPerGrade; }
    public IReadOnlyList<int> SpeedMultiplierPerGrade { get => speedMultiplierPerGrade; }
    public IReadOnlyList<int> PostAttackDelayMultiplierPerGrade { get => postAttackDelayMultiplierPerGrade; }
}
