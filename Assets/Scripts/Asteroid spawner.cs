using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidspawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array to hold asteroid prefabs
    public int numberOfAsteroids = 1000; // Number of asteroids to spawn
    public float spawnRadius = 1000f; // Radius within which asteroids will be spawned
    public Transform player; // Reference to the player's transform

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set in AsteroidSpawner script!");
            return;
        }

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;

            Vector3 spawnPosition = player.position + randomPosition;

            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            asteroidPrefab.transform.localScale = new Vector3(3,3,3);

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            asteroid.transform.parent = transform;
        }
    }
}
