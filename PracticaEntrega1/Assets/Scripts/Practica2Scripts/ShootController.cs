using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public LineRenderer laserLine; // Referencia al LineRenderer para el láser
    public float laserWidth = 0.1f; // Ancho del láser
    public float laserMaxLength = 100f; // Longitud máxima del láser
    public float laserDuration = 0.1f; // Duración del láser visual

    private InputManager inputManager;
    private bool AttackPressed;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;
    }

    void Update()
    {
        AttackPressed = inputManager.IsAttackPressed;

        if (AttackPressed) // Si es presiona la tecla de disparo
        {
            Disparar(); // Llama al método de disparo
        }
    }

    void Disparar()
    {
        RaycastHit hit; // Almacena información sobre la colisión del Raycast

        // Lanza un Raycast desde la posición del jugador en la dirección hacia adelante
        if (Physics.Raycast(transform.position, -transform.forward, out hit, laserMaxLength))
        {
            // Comprueba si el Raycast ha impactado en un enemigo
            if (hit.collider.CompareTag("Enemigo"))
            {
                // Muestra el láser en la dirección del raycast
                StartCoroutine(ShowLaser(transform.position, hit.point));

                // Realiza acciones adicionales cuando se golpea un enemigo
                // Puedes agregar aquí lógica para causar daño al enemigo, por ejemplo
                Debug.Log("Ha hiteado al enemigo");
            }
            else
            {
                // Muestra el láser en la dirección del raycast sin golpear un enemigo
                StartCoroutine(ShowLaser(transform.position, hit.point));
            }
        }
        else
        {
            // Si el raycast no golpea nada, muestra el láser hasta la distancia máxima
            Vector3 endPosition = transform.position - transform.forward * laserMaxLength;
            StartCoroutine(ShowLaser(transform.position, endPosition));
        }
    }

    // Método para mostrar visualmente el láser
    IEnumerator ShowLaser(Vector3 startPosition, Vector3 endPosition)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition(1, endPosition);
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}