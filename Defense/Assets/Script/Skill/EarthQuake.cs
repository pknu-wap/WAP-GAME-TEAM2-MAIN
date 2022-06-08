using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : APlayerSkill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.position = new Vector3(0, 1, 0);

    }
    public override void Use()
    {
        coolTime = 5f;
        if(particle != null)
        {
            StartCoroutine(ParticleEffect(particle));
        }
    }
}
