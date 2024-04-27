using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public float rangoDisparo = 100f; // Rang de dispar del Raycast

    void Update()
    {
        if (Input.GetButtonDown("tecla")) // Si es prem el botó de dispar 
        {
            Disparar(); // Crida al mètode de dispar
        }
    }

    void Disparar()
    {
        RaycastHit hit; // Guardarà la informació sobre la col·lisió del Raycast

        // Llança un raig des de la posició i direcció del jugador
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangoDisparo))
        {
            // Comprova si el raig colpeja un enemic 
            if (hit.collider.CompareTag("Enemic"))
            {
                // Si colpeja un enemic, li treurà vida etc...
                Debug.Log("Has colpejat un enemic!");
            }
        }
    }
}
