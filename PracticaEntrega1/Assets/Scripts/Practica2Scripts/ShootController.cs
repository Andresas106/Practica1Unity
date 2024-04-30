using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public LineRenderer laserLine; // Referencia al LineRenderer para el l�ser
    public float laserWidth = 0.1f; // Ancho del l�ser
    public float laserMaxLength = 100f; // Longitud m�xima del l�ser
    public float laserDuration = 0.1f; // Duraci�n del l�ser visual

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
            Disparar(); // Llama al m�todo de disparo
        }
    }

    void Disparar()
    {
        RaycastHit hit; // Almacena informaci�n sobre la colisi�n del Raycast

        // Lanza un Raycast desde la posici�n del jugador en la direcci�n hacia adelante
        if (Physics.Raycast(transform.position, -transform.forward, out hit, laserMaxLength))
        {
            // Comprueba si el Raycast ha impactado en un enemigo
            if (hit.collider.CompareTag("Enemigo"))
            {
                // Muestra el l�ser en la direcci�n del raycast
                StartCoroutine(ShowLaser(transform.position, hit.point));

                // Realiza acciones adicionales cuando se golpea un enemigo
                // Puedes agregar aqu� l�gica para causar da�o al enemigo, por ejemplo
                Debug.Log("Ha hiteado al enemigo");
            }
            else
            {
                // Muestra el l�ser en la direcci�n del raycast sin golpear un enemigo
                StartCoroutine(ShowLaser(transform.position, hit.point));
            }
        }
        else
        {
            // Si el raycast no golpea nada, muestra el l�ser hasta la distancia m�xima
            Vector3 endPosition = transform.position - transform.forward * laserMaxLength;
            StartCoroutine(ShowLaser(transform.position, endPosition));
        }
    }

    // M�todo para mostrar visualmente el l�ser
    IEnumerator ShowLaser(Vector3 startPosition, Vector3 endPosition)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition(1, endPosition);
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}