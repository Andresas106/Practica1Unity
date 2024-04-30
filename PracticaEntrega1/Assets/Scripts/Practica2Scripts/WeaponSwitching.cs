using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0, previousSelectedWeapon = 0;
    private InputManager inputManager;
    private bool Arma1Pressed;
    private bool Arma2Pressed;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        SelectWeapon();
    }

    void Update()
    {
        Arma1Pressed = inputManager.IsArma1Pressed;
        Arma2Pressed = inputManager.IsArma2Pressed;
        if (Arma1Pressed)
        {
            
            selectedWeapon=0;
        }
        if (Arma2Pressed)
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i==selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }else
                weapon.gameObject.SetActive(false);
            i++;
        }
        previousSelectedWeapon = selectedWeapon;
    }
}