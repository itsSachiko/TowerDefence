using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachable
{
    public TowerPlacer Placer { get; set; }
    public void Attach(Transform target, TowerPlacer towerPlacer);

    public void Revome(Vector3 targetPos);
}
