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
            var x = Instantiate(enemyPref);
            enemyList.Add(x.GetComponent<Enemy>());
            x.SetActive(false);
        }
    }
    public Enemy GetEnemy(Transform enemySpawner)
    {
        Enemy enemy;
        if (enemyList.Count <= 5)
        {
            var x = Instantiate(enemyPref, enemySpawner.position, Quaternion.identity);
            enemy = x.GetComponent<Enemy>();
        }
        else
        {
            enemy = enemyList[0];
            enemyList.RemoveAt(0);

        }
        enemy.transform.position = enemySpawner.position;
        enemy.baseTransform = baseTransform;
        enemy.gameObject.SetActive(true);
        return enemy;
    }
}
