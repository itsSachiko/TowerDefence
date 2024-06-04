using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTower : Tower
{
    public float explosionRange;
    public override void Attach(Transform target, TowerPlacer placer)
    {
        base.Attach(target, placer);
    }
    public override void Remove(Vector3 targetPos)
    {
        base.Remove(targetPos);
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void OnHitEffect(IHP iHP, Bullet bullet)
    {
        Collider[] colliders = Physics.OverlapSphere(bullet.transform.position, explosionRange, enemyMask);

        if (colliders.Length < 0)
        {
            return;
        }
        foreach (Collider coll in colliders)
        {

            if (coll.transform.TryGetComponent(out IHP hP))
            {
                buffs?.Invoke(hP);
                hP.TakeDamage(dmg);
            }
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
