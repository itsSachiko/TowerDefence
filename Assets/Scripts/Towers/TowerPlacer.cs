using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    public delegate void TowerEvent();
    public TowerEvent onTowerTick;

    private void Update()
    {
        onTowerTick?.Invoke();
    }
}
