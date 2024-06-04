using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP 
{
    public void TakeDamage(float dmg);

    public void Death();

    /// <summary>
    /// transform che uso per trovare l'enemy
    /// </summary>
    public Transform hpHolder { get; }
    public float HP { get; set; }

}
