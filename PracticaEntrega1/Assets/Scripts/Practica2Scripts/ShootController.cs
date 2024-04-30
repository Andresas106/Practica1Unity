using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject arrowPrefab; // Prefab de la flecha
    public Transform arrowSpawnPoint; // Punto de origen de la flecha
    public float arrowSpeed = 30f; // Velocidad de la flecha
    public float raycastDistance = 100f; // Rango de alcance del Raycast

    void Update()
    {
        if (Input.GetButtonDown("tecla")) // Si es presiona la tecla de disparo
        {
            Disparar(); // Llama al método de disparo
        }
    }

    void Disparar()
    {
        RaycastHit hit; // Almacena información sobre la colisión del Raycast

        // Lanza un Raycast desde la posición del jugador en la dirección hacia adelante
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Comprueba si el Raycast ha impactado en un enemigo
            if (hit.collider.CompareTag("Enemigo"))
            {
                // Instancia la flecha en el punto de origen y la dirección del jugador
                GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation);

                // Obtiene el componente Rigidbody de la flecha
                Rigidbody rb = arrow.GetComponent<Rigidbody>();

                // Si el componente Rigidbody existe
                if (rb != null)
                {
                    // Aplica fuerza a la flecha en la dirección hacia adelante del jugador
                    rb.velocity = transform.forward * arrowSpeed;
                }
            }
            else
            {
                Debug.Log("No hay enemigo en la línea de tiro.");
            }
        }
        else
        {
            Debug.Log("No hay objeto detectado en la línea de tiro.");
        }
    }
}