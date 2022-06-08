using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class APlayerSkill : MonoBehaviour
{
    protected ParticleSystem particle;
    protected Vector3 position;
    protected float coolTime;
    protected virtual void OnEnable()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public virtual void Use() { }
    public IEnumerator DelayTime(Button button)
    {
        float time = 0;
        button.enabled = false;
        Text buttonText = button.GetComponentInChildren<Text>();
        while(time <= coolTime)
        {
            time += Time.deltaTime;
            int remainTime = Mathf.CeilToInt(coolTime - time);
            buttonText.text = remainTime + "";
            yield return null;
        }
        buttonText.text = "Skill";
        button.enabled = true;
    }
    protected IEnumerator ParticleEffect(ParticleSystem particle)
    {
        particle.Play();
        yield return new WaitUntil(() => particle.isPlaying == false);
        Destroy(particle.gameObject);
    }
}
