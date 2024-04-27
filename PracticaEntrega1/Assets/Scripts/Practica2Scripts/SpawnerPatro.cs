using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPatro : MonoBehaviour
{
    public GameObject cubPrefab; // Prefab del cubo
    public int radiCercle = 5; // Radio del círculo
    public int numCubs = 20; // Número de cubos
    public float offsetCercle = 0.5f; // Espacio entre los cubos en el círculo
    public float offsetAltura = 1f; // Espacio entre los cubos apilados
    public float velocidadRotacion = 20f; // Velocidad de rotación de los cubos

    void Start()
    {
        ConstruirEstructuraCircular();
    }

    void ConstruirEstructuraCircular()
    {
        for (int i = 0; i < numCubs; i++)
        {
            // Calcular el ángulo para cada cubo alrededor del círculo
            float angle = i * Mathf.PI * 2f / numCubs;

            // Calcular las coordenadas X y Z del cubo a partir del ángulo y el radio del círculo
            float x = Mathf.Cos(angle) * radiCercle;
            float z = Mathf.Sin(angle) * radiCercle;

            // Crear la posición de spawn del cubo a partir de las coordenadas y la altura
            Vector3 posicioSpawn = new Vector3(x, i * offsetAltura, z);

            // Calcular la rotación del cubo para mirar hacia el centro del círculo
            Quaternion rotacio = Quaternion.LookRotation(transform.position - posicioSpawn);

            // Generar un cubo en la posición y rotación calculadas
            GameObject cub = Instantiate(cubPrefab, posicioSpawn, rotacio, transform);
            cub.transform.localScale = Vector3.one; // Tamaño base de 1 en todas las dimensiones

            cub.GetComponent<Renderer>().material.color = Random.ColorHSV(); // Color aleatorio

            // Añadir rotación continua al cubo alrededor del centro de la estructura
            cub.transform.RotateAround(transform.position, Vector3.up, i * (360f / numCubs));
        }
    }

    void Update()
    {
        // Rotar todos los cubos alrededor del centro de la estructura
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
    }
}
