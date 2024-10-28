using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float startSpawnRadius = 10f;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Wave currentWave;

    private float nextSpawnTime = 1f;
    private void Update()
    {

        spawnRadius = startSpawnRadius * Progression.difficulty;

        if (Time.time >= nextSpawnTime)
        {
            SpawnWave();
            nextSpawnTime = Time.time + 1f / currentWave.spawnRate;
        }
    }

    private void SpawnWave()
    {
        foreach (EnemyType eType in currentWave.enemies)
        {
            float spawnChance = eType.spawnChance * Progression.difficulty;

            if (Random.value <= spawnChance)
            {
                SpawnEnemy(eType.enemyPrefab);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (PlayerController.position != null)
        {
            Vector2 spawnPos = PlayerController.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

            ObjectPoolManager.SpawnObject(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);

        }
    }

}
