using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBase : MonoBehaviour, IHP
{
    public static Action lose;
    public float hp;
    public Transform hpHolder => transform;

    public float HP { get => hp; set => hp = value; }


    public void Death()
    {
        lose?.Invoke();
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Death();
        }
    }
}
