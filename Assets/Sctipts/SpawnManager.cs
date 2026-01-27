using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform cameraTransform; 
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;             
    public float spawnInterval = 2f;    
    [SerializeField] float SpawnTime;
    public int maxEnemies = 5; 
    [SerializeField] Transform[] spawnPos;
    [Header("SpawnFall Ob Settings")]
    [SerializeField] GameObject[] objectPrefap;


    private float timer;
    private float timerFall;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;
        timerFall += Time.deltaTime;

        if (timer >= spawnInterval && !GameManager.instance.isdead )
        {
            TrySpawnEnemy();
            timer = 0f;
        }
        if(timerFall >= SpawnTime && !GameManager.instance.isdead)
        {
          FallObstacles();
          timerFall = 0;
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
    void FallObstacles ()
    {
        int rngOb = Random.Range(0,objectPrefap.Length);
        int ranPos = Random.Range(0,spawnPos.Length);
        Instantiate(objectPrefap[rngOb],spawnPos[Random.Range(0, spawnPos.Length)].transform.position, Quaternion.identity);
    }
}
