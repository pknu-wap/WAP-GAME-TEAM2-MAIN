using System.Collections;
using UnityEngine;

public abstract class AAttackBehaviour : MonoBehaviour
{
    private double originDamage;
    private double originRange;
    private float originPostAttackDelay;
    private float originPreAttackDelay;

    protected double damage;
    protected double range;
    protected float postAttackDelay;
    protected float preAttackDelay;

    private Coroutine AttackRoutine;


    public bool IsOnAttacking
    {
        private set; get;
    }

    public double OriginDamage { get => originDamage; }
    public double OriginRange { get => originRange; }
    public float OriginPostAttackDelay { get => originPostAttackDelay;  }
    public float OriginPreAttackDelay { get => originPreAttackDelay; }

    public void Init(double damage, double range, float postAttackDelay, float preAttackDelay)
    {
        this.originDamage = damage;
        this.originRange = range;
        this.postAttackDelay = postAttackDelay;
        this.preAttackDelay = preAttackDelay;
        OnEnable();
    }

    private void OnEnable()
    {
        damage = originDamage;
        range = originDamage;
        preAttackDelay = originPreAttackDelay;
        postAttackDelay = originPostAttackDelay;
    }

    public void OrderAttack(GameObject target)
    {
        if (IsOnAttacking) return;

        AttackRoutine = StartCoroutine(AttackProcess(target));
    }

    private IEnumerator AttackProcess(GameObject target)
    {
        IsOnAttacking = true;
        yield return new WaitForSeconds(preAttackDelay);
        Attack(target);
        yield return new WaitForSeconds(postAttackDelay);
        IsOnAttacking = false;
    }

    protected abstract void Attack(GameObject target);
}
