using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public float rangoDisparo = 100f; // Rang de dispar del Raycast

    void Update()
    {
        if (Input.GetButtonDown("tecla")) // Si es prem el bot� de dispar 
        {
            Disparar(); // Crida al m�tode de dispar
        }
    }

    void Disparar()
    {
        RaycastHit hit; // Guardar� la informaci� sobre la col�lisi� del Raycast

        // Llan�a un raig des de la posici� i direcci� del jugador
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangoDisparo))
        {
            // Comprova si el raig colpeja un enemic 
            if (hit.collider.CompareTag("Enemic"))
            {
                // Si colpeja un enemic, li treur� vida etc...
                Debug.Log("Has colpejat un enemic!");
            }
        }
    }
}
