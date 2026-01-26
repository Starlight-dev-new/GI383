using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform cameraTransform; 
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;             
    public float spawnInterval = 2f;    
    public int maxEnemies = 5; 
    [SerializeField] Transform[] spawnPos;

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
    void LateUpdate()
    {
        transform.position = new Vector3(0, cameraTransform.position.y + 10f, 0);
    }

    void TrySpawnEnemy()
    {
        if (activeEnemies.Count >= maxEnemies) return;
        
        GameObject enemy = Instantiate(enemyPrefab, spawnPos[Random.Range(0, spawnPos.Length)].transform.position, Quaternion.identity);
        activeEnemies.Add(enemy);
    }
}
