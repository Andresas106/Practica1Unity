using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    // Indica qu� arma est� seleccionada actualmente y la anterior
    public int selectedWeapon = 0, previousSelectedWeapon = 0;

    // Referencia al sistema de entrada para obtener comandos de usuario
    private InputManager inputManager;

    // Variables para detectar qu� teclas se est�n presionando para cambiar de arma
    private bool Arma1Pressed;
    private bool Arma2Pressed;

    void Start()
    {
        inputManager = GetComponent<InputManager>();// Obtener la referencia al sistema de entrada
        SelectWeapon(); // Seleccionar el arma inicial
    }

    void Update()
    {
        Arma1Pressed = inputManager.IsArma1Pressed;// Ver si la tecla para arma 1 est� presionada
        Arma2Pressed = inputManager.IsArma2Pressed; // Ver si la tecla para arma 2 est� presionada

        // Cambiar el arma seleccionada seg�n la entrada del usuario
        if (Arma1Pressed)
        {
            
            selectedWeapon=0;// Seleccionar el primer arma
        }
        if (Arma2Pressed)
        {
            selectedWeapon = 1;// Seleccionar el segundo arma
        }

        // Si el arma seleccionada cambi�, actualiza
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();// Cambiar el arma activa
        }

        // Detectar entrada del scroll del rat�n
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            // Scroll hacia arriba, avanzar al siguiente arma
            selectedWeapon = (selectedWeapon + 1) % transform.childCount;
        }
        else if (scroll < 0f)
        {
            // Scroll hacia abajo, retroceder al arma anterior
            selectedWeapon = (selectedWeapon - 1 + transform.childCount) % transform.childCount;
        }

        // Verificar si el arma seleccionada cambi� despu�s de la entrada del scroll
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();// Actualizar la selecci�n de armas
        }
    }


    // M�todo para seleccionar el arma activa
    void SelectWeapon()
    {
        // Recorre todos los hijos del GameObject para activar/desactivar armas
        int i = 0;
        foreach(Transform weapon in transform)
        {
            // Activar el arma seleccionada y desactivar las dem�s
            if (i==selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }else
                weapon.gameObject.SetActive(false);
            i++;// Incrementar el �ndice para recorrer todas las armas
        }
        // Actualizar el arma seleccionada previamente
        previousSelectedWeapon = selectedWeapon;
    }
}