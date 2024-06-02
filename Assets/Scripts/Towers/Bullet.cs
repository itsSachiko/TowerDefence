using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

        BulletPooler.bulletList.Add(this); 
    }
}
