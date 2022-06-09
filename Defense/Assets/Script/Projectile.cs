using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Projectile : MonoBehaviour
{
    private float speedPerSec;
    private Transform targetObj;
    private Transform startObj;
    private double damage;
    [SerializeField]
    private float collideDistance = 1f;

    public void Init(float speedPerSec,double damage, Transform targetObj, Transform startObj)
    {
        this.speedPerSec = speedPerSec;
        this.targetObj = targetObj;
        this.startObj = startObj;
        this.damage = damage;
    }

    protected virtual void Update()
    {
        float moveDistanceScalar = speedPerSec * Time.deltaTime;
        Vector3 moveVector = (targetObj.position - startObj.position).normalized;
        transform.position += moveVector * moveDistanceScalar;

        if ((transform.position - targetObj.position).magnitude <= collideDistance) OnArrive();
    }

    protected virtual void OnArrive()
    {
        gameObject.SetActive(false);
        if (targetObj.gameObject.activeSelf)
        {
            targetObj.GetComponent<EntityHealth>().TakeDamage(damage);
            GetComponent<PooledObject>().ReturnToPool();
        }
    }
}
