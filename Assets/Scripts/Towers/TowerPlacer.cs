using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    public delegate void TowerEvent();
    public TowerEvent onTowerTick;

    public Action<IHP> buffEffect;
    public Action onAttach;
    private void Update()
    {
        onTowerTick?.Invoke();
    }

}
