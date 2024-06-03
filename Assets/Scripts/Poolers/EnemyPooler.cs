using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public static List<Enemy> enemyList = new List<Enemy>();
    public float numberOfEnemy;
    public GameObject enemyPref;
    public Transform baseTransform;

    private void Awake()
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            var enemyInstance = Instantiate(enemyPref);
            enemyList.Add(enemyInstance.GetComponent<Enemy>());
            enemyInstance.SetActive(false);
        }
    }
    public void GetEnemy(Transform enemySpawner)
    {
        Enemy enemy;
        if (enemyList.Count == 0)
        {
            var enemyInstance = Instantiate(enemyPref, enemySpawner.position, Quaternion.identity);
            enemy = enemyInstance.GetComponent<Enemy>();
            enemyInstance.SetActive(false);
        }
        else
        {
            enemy = enemyList[0];
            enemyList.RemoveAt(0);
        }

        enemy.transform.position = enemySpawner.position;
        enemy.baseTransform = baseTransform;
        enemy.gameObject.SetActive(true);
    }
}