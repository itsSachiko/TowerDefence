using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour, IAttachable
{
    public TowerPlacer Placer { get; set; }
    [HideInInspector] public Enemy hitEnemy;
    protected Vector3 startPos;
    
    public virtual void Attach(Transform target, TowerPlacer towerPlacer)
    {
        this.startPos = this.transform.position;
        this.Placer = towerPlacer;
        this.Placer.buffEffect += this.OnHitEffect;
        this.Placer.onAttach?.Invoke();
        
        if (this.Placer.onTowerTick == null)
        {
            Remove(startPos - Vector3.up);
        }
        else
        {
            this.transform.position = target.position + Vector3.up;
        }
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<IAttachable>(out _))
            {
                hit.transform.gameObject.layer = 7;
            }
        }
    }

    public virtual void Remove(Vector3 targetPos)
    {
        this.Placer.buffEffect -= this.OnHitEffect;
        this.Placer.onAttach?.Invoke();
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

    /// <summary>
    /// ciò che afflige ai nemici
    /// </summary>
    /// <param name="hP"></param>
    public virtual void OnHitEffect(IHP hP)
    {
        hP.hpHolder.TryGetComponent(out this.hitEnemy);
    }
}
