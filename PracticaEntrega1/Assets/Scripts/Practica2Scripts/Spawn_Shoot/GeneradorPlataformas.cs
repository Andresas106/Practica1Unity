using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPlataformas : MonoBehaviour
{
    public GameObject plataformaPrefab; // El prefab de la plataforma que queremos generar
    public Transform puntoDeGeneracion; // El punto desde donde queremos generar la plataforma
    public float distanciaEntrePlataformas; // La distancia horizontal entre las plataformas generadas

    private float plataformaLength; // La longitud de la plataforma

    void Start()
    {
        plataformaLength = plataformaPrefab.GetComponent<MeshRenderer>().bounds.size.z; // Obtener la longitud de la plataforma
    }

    void Update()
    {
        // Si la posici�n actual del jugador es menor que la posici�n de generaci�n, generar una nueva plataforma
        if (transform.position.z < puntoDeGeneracion.position.z)
        {
            // Instanciar una nueva plataforma en la posici�n de generaci�n
            Instantiate(plataformaPrefab, new Vector3(puntoDeGeneracion.position.x, puntoDeGeneracion.position.y, puntoDeGeneracion.position.z - plataformaLength - distanciaEntrePlataformas), Quaternion.identity);

            // Mover el punto de generaci�n a la nueva posici�n
            puntoDeGeneracion.position = new Vector3(puntoDeGeneracion.position.x, puntoDeGeneracion.position.y, puntoDeGeneracion.position.z - plataformaLength - distanciaEntrePlataformas);
        }
    }
}


