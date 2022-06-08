using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttack : MonoBehaviour
{
    [SerializeField] GameObject longAttackPrefab;
    [SerializeField] ParticleSystem shortEffect;
    private GameObject attackPoint;
    private float touchTime;
    void Update()
    {
        if(GameManager.Instance.IsStart) InputEventManager.Instance.AddTouchEvent(Attack);
    }
    private void Attack(Touch touch)
    {
        Vector3 position = FieldManager.Instance.GetWorldPosition(touch.position);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchTime = 0; 
                attackPoint = Instantiate(longAttackPrefab, position, Quaternion.identity);
                
;                break;
            case TouchPhase.Moved:
                touchTime += Time.deltaTime;
                if (touchTime > 0.05f) LongAttack(position);
                break;

            case TouchPhase.Ended:
                if (touchTime <= 0.05f) ShortAttack(position);
                break;

        }
    }
    private void LongAttack(Vector3 position)
    {
        attackPoint.transform.position = position;
    }
    private void ShortAttack(Vector3 position)
    {
        StartCoroutine(GameManager.Instance.ParticleEffect(shortEffect, position));
    }
}
