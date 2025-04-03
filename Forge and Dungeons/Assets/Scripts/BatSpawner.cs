using System.Collections;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{

    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform player; // Referencia al jugador
    public Camera playerCamera; // Cámara del jugador

    public int minEnemiesPerSpawn = 1;
    public int maxEnemiesPerSpawn = 3;
    public float spawnIntervalMin = 10f;
    public float spawnIntervalMax = 15f;

    private float spawnDistance = 8f; // Distancia alrededor de la cámara donde spawnearán los enemigos
    private float spawnHeight = 2f; // Altura a la que aparecerán los murciélagos

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            int enemiesToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 cameraPos = playerCamera.transform.position;
        float cameraSizeY = playerCamera.orthographicSize;
        float cameraSizeX = cameraSizeY * playerCamera.aspect;

        float spawnX, spawnZ;

        bool spawnOnX = Random.value > 0.5f; // Decide si spawnea en los lados X o Z

        if (spawnOnX)
        {
            spawnX = Random.Range(cameraPos.x - cameraSizeX - spawnDistance, cameraPos.x + cameraSizeX + spawnDistance);
            spawnZ = cameraPos.z + (Random.value > 0.5f ? spawnDistance : -spawnDistance);
        }
        else
        {
            spawnX = cameraPos.x + (Random.value > 0.5f ? spawnDistance : -spawnDistance);
            spawnZ = Random.Range(cameraPos.z - cameraSizeY - spawnDistance, cameraPos.z + cameraSizeY + spawnDistance);
        }

        return new Vector3(spawnX, player.position.y + spawnHeight, spawnZ); // Spawnea en el aire
    }
}
