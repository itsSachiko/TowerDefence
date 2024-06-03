using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHP
{
    public Transform baseTransform;
    public Transform currentTransform;
    NavMeshAgent agent;
    public float hp;
    public float HP { get => hp; set => hp = value; }
    public Transform hpHolder { get => transform; }

    [HideInInspector] public Vector3 startPos;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        if (baseTransform == null)
        {
            return;
        }

        startPos = transform.position;
        UpdateDestination(baseTransform.position);
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
        Debug.Log(agent.destination);
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
}
