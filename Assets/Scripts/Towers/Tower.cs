using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttachable
{
    public float fireRate;
    public float dmg;
    public float range;
    float timer;


    public GameObject bullet;

    Transform targetEnemy;


    public TowerPlacer Placer { get; set; }
    public virtual void Attach(Transform target, TowerPlacer placer)
    {
        this.Placer = placer;
        Placer.onTowerTick += Shoot;

        this.transform.position = target.position + Vector3.up;
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<IAttachable>(out _))
            {
                hit.transform.gameObject.layer = 7;
            }
        }
    }

    public virtual void Revome(Vector3 targetPos)
    {
        this.Placer.onTowerTick -= Shoot;
        if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit))
        {

            if (hit.transform.TryGetComponent<IAttachable>(out _))
            {
                hit.transform.gameObject.layer = 0;
            }
        }

        this.transform.position = targetPos + Vector3.up;
        this.Placer = null;
    }

    public virtual void Shoot()
    {
        if(targetEnemy == null)
        {
            RangeCheck();
        }

        else
        {

        }


    }

    private void RangeCheck()
    {
        Collider[] coll = Physics.OverlapSphere(this.transform.position, this.range);
        if(coll != null)
        {
            targetEnemy = coll[0].transform;
        }
    }
}
