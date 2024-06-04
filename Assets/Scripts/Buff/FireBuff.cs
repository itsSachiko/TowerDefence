using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    public float damageOverTime;
    public float timer;
    public override void Attach(Transform target, TowerPlacer towerPlacer)
    {
        base.Attach(target, towerPlacer);
    }

    public override void Remove(Vector3 targetPos)
    {
        base.Remove(targetPos);
    }
    public override void OnHitEffect(IHP hP)
    {
        StartCoroutine(Burn(hP));

    }

    IEnumerator Burn(IHP hP)
    {
        float t = 0;
        
        while(t < timer)
        {
            hP.TakeDamage(damageOverTime * Time.deltaTime);
            yield return null;
        }    
    }
}
