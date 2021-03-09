using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 9.0f;
    private float xRange;
    private float zRange;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, SpawnRandomPosition(), powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
		{
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerUpPrefab, SpawnRandomPosition(), powerUpPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemiesSpawn)
	{
        for (int i = 0; i < enemiesSpawn ; i++)
		{
            Instantiate(enemyPrefab, SpawnRandomPosition(), enemyPrefab.transform.rotation);
        }
	}

    private Vector3 SpawnRandomPosition()
	{
        xRange = Random.Range(-spawnRange, spawnRange);
        zRange = Random.Range(-spawnRange, spawnRange);
        return (new Vector3(xRange, 0, zRange));
    }
}
