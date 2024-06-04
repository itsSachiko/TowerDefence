using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHP
{
    [HideInInspector] public Transform baseTransform;
    [HideInInspector] public NavMeshAgent agent;
    public float hp;
    public float HP { get => hp; set => hp = value; }
    public Transform hpHolder { get => transform; }

    [HideInInspector] public Vector3 startPos;

    public float speed;
    float ogHP;
    public float damage;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ogHP = hp;
    }
    private void OnEnable()
    {
        if (baseTransform == null)
        {
            return;
        }

        startPos = transform.position;
        UpdateDestination(baseTransform.position);
        agent.speed = speed;
        hp = ogHP;
    }

    private void OnDisable()
    {
        if (baseTransform == null)
        {
            return;
        }

        EnemyPooler.enemyList.Add(this);
    }
    public void UpdateDestination(Vector3 newDestination)
    {
        agent.destination = newDestination;
    }

    public void Death()
    {
        transform.position = Vector3.one * -10;
        gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Death();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHP hP))
        {
            hP.TakeDamage(damage);
            Death();
        }
    }
}
