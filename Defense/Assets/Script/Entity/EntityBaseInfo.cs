using UnityEngine;

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
    [Space]
    [SerializeField]
    private double movementSpeed;
    [Space]
    [SerializeField]
    private double healthPoint;

    public GameObject EntityPrefab { get => entityPrefab; }
    public double Damage { get => damage; }
    public double Range { get => range;  }
    public float PostAttackDelay { get => postAttackDelay; }
    public float PreAttackDelay { get => preAttackDelay;  }
    public double MovementSpeed { get => movementSpeed; }
    public double HealthPoint { get => healthPoint;  }
}
