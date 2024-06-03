using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHP
{
    [SerializeField] Transform basePos;
    NavMeshAgent agent;
    public float hp;
    public float HP { get => hp ; set => hp = value; }
    public Transform hpHolder { get => transform; }

    public Vector3 currentDestination;

    public Vector3 dir;
    public void UpdateDestination(Vector3 newDestination)
    {
        agent.destination = newDestination;

    }
    private void Update()
    {
        dir = (basePos.position - transform.position).normalized;
    }

    public void Death()
    {
        transform.position = Vector3.one * -10;
        gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0 )
        {
            Death();

        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination(basePos.position);
    }
}
