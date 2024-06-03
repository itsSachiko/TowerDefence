using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionTower : Tower
{
    public float confusionDuration;
    public override void Attach(Transform target, TowerPlacer placer)
    {
        base.Attach(target, placer);
    }
    public override void Revome(Vector3 targetPos)
    {
        base.Revome(targetPos);
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void OnHitEffect(IHP iHP, Bullet bullet)
    {
        base.OnHitEffect(iHP, bullet);
        if(iHP.hpHolder.TryGetComponent(out Enemy enemy))
        {
            StartCoroutine(ConfusionEffect(enemy)); 
        }
    }

    IEnumerator ConfusionEffect(Enemy enemy)
    {
        enemy.UpdateDestination(enemy.startPos);
        yield return new WaitForSeconds(confusionDuration);
        enemy.UpdateDestination(enemy.baseTransform.position);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, explosionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
