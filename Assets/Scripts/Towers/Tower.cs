using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour, IAttachable
{
    public float fireRate;
    public float dmg;
    public float range;
    protected float timer = 0;

    public LayerMask enemyMask;
    protected Transform targetEnemy;

    public BulletPooler bulletPooler;

    protected Bullet currentBullet;

    public Action<IHP> buffs;

    public TowerPlacer Placer { get; set; }
    public virtual void Attach(Transform target, TowerPlacer placer)
    {
        this.timer = 0;
        this.Placer = placer;
        Placer.onTowerTick += Shoot;
        Placer.onAttach += UpdateBuffs;
        UpdateBuffs();
        this.transform.position = target.position + Vector3.up;
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<IAttachable>(out _))
            {
                hit.transform.gameObject.layer = 7;
            }
        }

    }

    public virtual void Remove(Vector3 targetPos)
    {
        this.Placer.onTowerTick -= Shoot;
        Placer.onAttach -= UpdateBuffs;
        UpdateBuffs();
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
        
        if(this.targetEnemy == null || Vector3.Distance(this.targetEnemy.position, this.transform.position ) > this.range || this.targetEnemy.gameObject.activeSelf == false)
        {
            this.RangeCheck();
        }

        else
        {
            Vector3 dir = targetEnemy.position - this.transform.position;
            dir.y = transform.position.y;
            dir = dir.normalized;
            float angle = Vector3.Angle(this.transform.forward, dir);
            transform.rotation = Quaternion.LookRotation(dir);
            if(this.timer < this.fireRate)
            {
                this.timer += Time.deltaTime;
                return;
            }

            this.currentBullet = this.bulletPooler.GetBullet(this.transform, this.targetEnemy);
            this.currentBullet.onHit = this.OnHitEffect;
            this.timer = 0;
        }

    }

    public virtual void OnHitEffect(IHP iHP, Bullet bullet)
    {
        iHP.TakeDamage(dmg);
        buffs?.Invoke(iHP);
    }

    public void RangeCheck()
    {
        Collider[] coll = Physics.OverlapSphere(this.transform.position, this.range, enemyMask);
        if(coll.Length > 0)
        {
            this.targetEnemy = coll[0].transform;
        }
    }

    public void UpdateBuffs()
    {
        buffs = Placer.buffEffect;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
