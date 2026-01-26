using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;      
    public Transform player;            
    public float distanceAbovePlayer = 8f; 
    public float spawnInterval = 2f;    
    public int maxEnemies = 5;           

    private float timer;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnEnemy();
            timer = 0f;
        }

        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    void TrySpawnEnemy()
    {
        if (activeEnemies.Count >= maxEnemies) return;

        Vector3 spawnPos = new Vector3(
            player.position.x,
            player.position.y + distanceAbovePlayer,
            0f
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        activeEnemies.Add(enemy);
    }
}
