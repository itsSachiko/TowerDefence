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
        
        float confusionTimer = 0f;
        while(confusionDuration > confusionTimer)
        {
            confusionTimer += Time.deltaTime;
            enemy.UpdateDestination(-enemy.dir);
            yield return null;
        }


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
