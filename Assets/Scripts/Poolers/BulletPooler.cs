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
    public Bullet GetBullet(Transform towerTransform, Transform target)
    {
        Bullet bull;
        if (bulletList.Count == 0)
        {
            var x = Instantiate(bulletPref, towerTransform.position, Quaternion.identity);
            bull = x.GetComponent<Bullet>();
        }
        else
        {
            bull = bulletList[0];
            bulletList.RemoveAt(0);

        }
        bull.transform.position = towerTransform.position;
        bull.target = target;
        bull.gameObject.SetActive(true);
        return bull;
    }
}
