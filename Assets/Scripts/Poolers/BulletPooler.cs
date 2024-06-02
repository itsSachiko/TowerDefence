using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static List<Bullet> bulletList = new List<Bullet>();
    public float numberOfBullets;
    public GameObject bulletPref;
    private void Awake()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            var x = Instantiate(bulletPref);
            bulletList.Add(x.GetComponent<Bullet>());
            x.SetActive(false);
        }
    }
    public Bullet GetBullet(Transform transform, Transform target)
    {
        Bullet bull;
        if (bulletList.Count == 0)
        {
            var x = Instantiate(bulletPref, transform.position, Quaternion.identity);
            bull = x.GetComponent<Bullet>();
        }
        else
        {
            bull = bulletList[0];
            bull.gameObject.SetActive(true);
            bulletList.RemoveAt(0);

        }

        bull.target = target;
        return bull;
    }
}
