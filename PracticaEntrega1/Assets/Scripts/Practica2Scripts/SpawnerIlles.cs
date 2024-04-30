using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPatro : MonoBehaviour
{
    public GameObject islandPrefab; // Prefab de la isla
    public int circleRadius = 5; // Radio del círculo
    public int numIslands = 20; // Número de islas
    public float circleOffset = 0.5f; // Espacio entre las islas en el círculo
    public float heightOffset = 1f; // Espacio entre las islas apiladas
    public float rotationSpeed = 20f; // Velocidad de rotación de las islas

    void Start()
    {
        BuildCircularStructure();
    }

    void BuildCircularStructure()
    {
        for (int i = 0; i < numIslands; i++)
        {
            // Calcular el ángulo para cada isla alrededor del círculo
            float angle = i * Mathf.PI * 2f / numIslands;

            // Calcular las coordenadas X y Z de la isla a partir del ángulo y el radio del círculo
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;

            // Crear la posición de spawn de la isla a partir de las coordenadas y la altura
            Vector3 spawnPosition = new Vector3(x, i * heightOffset, z);

            // Calcular la rotación de la isla para mirar hacia el centro del círculo
            Quaternion rotation = Quaternion.LookRotation(transform.position - spawnPosition);

            // Generar una isla en la posición y rotación calculadas
            GameObject island = Instantiate(islandPrefab, spawnPosition, rotation, transform);
            island.transform.localScale = Vector3.one; // Tamaño base de 1 en todas las dimensiones


            // Añadir rotación continua a la isla alrededor del centro de la estructura
            island.transform.RotateAround(transform.position, Vector3.up, i * (360f / numIslands));
        }
    }

    void Update()
    {
        // Rotar todas las islas alrededor del centro de la estructura
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
