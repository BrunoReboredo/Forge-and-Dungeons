using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BatSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public Camera playerCamera;

    public int minEnemiesPerSpawn = 2;
    public int maxEnemiesPerSpawn = 5;
    public float spawnIntervalMin = 8f;
    public float spawnIntervalMax = 15f;

    private float spawnDistance = 8f; // Distancia alrededor de la cámara donde spawnearán los enemigos
    private float spawnHeight = 2f; // Altura a la que aparecerán los murciélagos
    private float maxAttempts = 10f; // Número máximo de intentos para encontrar una posición válida //TESTEO

    void Start()
    {
        StartCoroutine(SpawnBats());
    }

    IEnumerator SpawnBats()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            int enemiesToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnBat();
            }
        }
    }

    void SpawnBat()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        NavMeshHit hit;
        int attempts = 0;

        // Intenta encontrar una posición válida para el enemigo
        while (attempts < maxAttempts)
        {
            if (NavMesh.SamplePosition(spawnPosition, out hit, 10.0f, NavMesh.AllAreas)) // Aumentar el rango de búsqueda
            {
                spawnPosition = hit.position; // Ajustar a la posición válida en el suelo
                spawnPosition.y += spawnHeight; // Elevar al enemigo

                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return; // Si encontró una posición válida, instanciar el enemigo y salir
            }
            else
            {
                // Si no se encuentra una posición válida, recalcular la posición
                spawnPosition = GetSpawnPosition();
                attempts++; // Incrementar el contador de intentos
            }
        }
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

        // Establecer la posición inicial en el suelo (y = 0)
        return new Vector3(spawnX, 90f, spawnZ); // Spawnea a nivel del suelo (y = 90)
    }
}
