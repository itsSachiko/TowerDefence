using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float waveCooldown;
    public float spawnDelay;
    public float numberOfEnemies = 1;
    public EnemyPooler enemyPooler;

    private void Awake()
    {
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        float counterOfEnemies = 0;

        while (counterOfEnemies < numberOfEnemies)
        {
            counterOfEnemies++;
            enemyPooler.GetEnemy(transform);
            yield return new WaitForSeconds(spawnDelay);
        }
        numberOfEnemies++;
        yield return new WaitForSeconds(waveCooldown);
        StartCoroutine(SpawnWave());
    }
}
