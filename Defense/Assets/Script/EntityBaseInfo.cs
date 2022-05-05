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
}
