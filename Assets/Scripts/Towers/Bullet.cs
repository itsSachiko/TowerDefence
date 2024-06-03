using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public Action<IHP, Bullet> onHit;

    Vector3 startPos;

    float timer;
    public float duration = 0.5f;
    private void OnEnable()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {

        Vector3 pos = transform.position;
        if (timer < duration)
        {
            pos = Vector3.Lerp(startPos, target.position, timer / duration);
            timer += Time.fixedDeltaTime;
        }
        transform.position = pos;

    }

    private void OnDisable()
    {
        BulletPooler.bulletList.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHP hp))
        {
            onHit?.Invoke(hp,this);
            gameObject.SetActive(false);
        }
    }
}
