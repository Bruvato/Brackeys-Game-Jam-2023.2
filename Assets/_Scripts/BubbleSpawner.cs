using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float spawnInterval = 5f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector2 randomPosition = GetRandomPositionInCameraView();
            ObjectPoolManager.SpawnObject(bubblePrefab, randomPosition, bubblePrefab.transform.rotation, ObjectPoolManager.PoolType.GameObject);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector2 GetRandomPositionInCameraView()
    {
        float minX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        // float minY = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        // float maxY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        float spawnY = cam.ViewportToWorldPoint(new Vector3(0, -0.1f, 0)).y; // Spawn just below the screen

        float randomX = Random.Range(minX, maxX);
        // float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, spawnY);
    }
}
