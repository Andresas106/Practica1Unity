using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAxeDamage : MonoBehaviour
{
    public GameObject axe;
    private Collider axeCollider; // Referencia al collider del hacha

    void Start()
    {
        // Obtener el collider del hacha
        axeCollider = axe.GetComponent<Collider>();
    }

    void Update()
    {
        // Verificar si IsOnRange es verdadero
        if (GetComponent<Animator>().GetBool("IsAttacking"))
        {
            // Activar el collider del hacha
            if (axeCollider != null)
            {
                axeCollider.enabled = true;
            }
        }
        else
        {
            // Desactivar el collider del hacha si IsOnRange es falso
            if (axeCollider != null)
            {
                axeCollider.enabled = false;
            }
        }
    }
}