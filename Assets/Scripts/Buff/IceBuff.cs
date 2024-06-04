using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBuff : Buff
{
    public float removeSpeed;
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
        base.OnHitEffect(hP);
        StartCoroutine(SpeedDown());

    }

    IEnumerator SpeedDown()
    {
        float originalSpeed = hitEnemy.agent.speed;
        hitEnemy.agent.speed -= removeSpeed;
        yield return new WaitForSeconds(timer);
        hitEnemy.agent.speed += removeSpeed;
    }
}
