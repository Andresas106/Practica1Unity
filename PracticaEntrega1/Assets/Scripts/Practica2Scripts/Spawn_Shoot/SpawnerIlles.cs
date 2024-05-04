using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPatro : MonoBehaviour
{
    public GameObject islandPrefab; // Prefab de la isla
    public int circleRadius = 5; // Radio del c�rculo
    public int numIslands = 20; // N�mero de islas
    public float circleOffset = 0.5f; // Espacio entre las islas en el c�rculo
    public float heightOffset = 1f; // Espacio entre las islas apiladas
    public float rotationSpeed = 20f; // Velocidad de rotaci�n de las islas

    void Start()
    {
        BuildCircularStructure();
    }

    void BuildCircularStructure()
    {
        for (int i = 0; i < numIslands; i++)
        {
            // Calcular el �ngulo para cada isla alrededor del c�rculo
            float angle = i * Mathf.PI * 2f / numIslands;

            // Calcular las coordenadas X y Z de la isla a partir del �ngulo y el radio del c�rculo
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;

            // Crear la posici�n de spawn de la isla a partir de las coordenadas y la altura
            Vector3 spawnPosition = new Vector3(x, i * heightOffset, z);

            // Calcular la rotaci�n de la isla para mirar hacia el centro del c�rculo
            Quaternion rotation = Quaternion.LookRotation(transform.position - spawnPosition);

            // Generar una isla en la posici�n y rotaci�n calculadas
            GameObject island = Instantiate(islandPrefab, spawnPosition, rotation, transform);
            island.transform.localScale = Vector3.one; // Tama�o base de 1 en todas las dimensiones


            // A�adir rotaci�n continua a la isla alrededor del centro de la estructura
            island.transform.RotateAround(transform.position, Vector3.up, i * (360f / numIslands));
        }
    }

    void Update()
    {
        // Rotar todas las islas alrededor del centro de la estructura
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
