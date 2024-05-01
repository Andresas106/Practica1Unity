using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject poma;
    public float spawnInterval = 5f;
    float timer;

    // Parámetros adicionales para el Instantiate
    public Vector3 spawnOffset = Vector3.zero;
    public Quaternion spawnRotation = Quaternion.identity;
    public bool randomizeRotation = false; // Rotación aleatoria

    void Update()
    {
        spawnPoma();
    }

    void spawnPoma()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            Vector3 finalSpawnPosition = transform.position + spawnOffset + randomOffset;
            Quaternion finalSpawnRotation = randomizeRotation ? randomRotation : spawnRotation;

            GameObject newPoma = Instantiate(poma, finalSpawnPosition, finalSpawnRotation);

            timer = 0f;
        }
    }
}

