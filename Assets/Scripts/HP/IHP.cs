using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP 
{

    public void TakeDamage(float dmg);

    public void Death();
    public float HP { get; set; }

}
