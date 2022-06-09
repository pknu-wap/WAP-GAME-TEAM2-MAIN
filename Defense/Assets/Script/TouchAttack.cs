using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttack : MonoBehaviour
{
    [SerializeField] GameObject longTouch;
    [SerializeField] ParticleSystem shortTouch;
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
                attackPoint = Instantiate(longTouch, position, Quaternion.identity);
                
;                break;
            case TouchPhase.Moved:
                touchTime += Time.deltaTime;
                if (touchTime > 0.05f) attackPoint.GetComponent<LongTouch>().MoveToTouch(position);
                break;

            case TouchPhase.Ended:
                if (touchTime <= 0.05f)
                {
                    StartCoroutine(GameManager.Instance.ParticleEffect(shortTouch, position));
                }
                Destroy(attackPoint, 1);
                break;

        }
    }

}
