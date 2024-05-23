using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Animator animator;
    float _timer;
    bool isTheEnd; // Bandera para verificar si Happy() fue llamado

    void Start()
    {
        animator = GetComponent<Animator>();
        _timer = 0f; // Inicializar _timer
        isTheEnd = false; // Inicializar la bandera
    }

    void Update()
    {
        if (isTheEnd)
        {
            _timer += Time.deltaTime; // Incrementar _timer con deltaTime

            if (_timer > 4)
            {
                animator.SetBool("Happy", false);
                animator.SetBool("Angry", false);
                animator.SetBool("Victory", false);
                animator.SetBool("Defeat", false);

                isTheEnd = false; // Resetear la bandera
                _timer = 0f; // Reiniciar el temporizador
            }
        }
    }

    public void Happy()
    {
        animator.SetBool("Happy", true);

        isTheEnd = true; // Activar la bandera
        _timer = 0f; // Reiniciar el temporizador al llamar Happy()
    }
    public void Angry()
    {
        animator.SetBool("Angry", true);

        isTheEnd = true; // Activar la bandera
        _timer = 0f; // Reiniciar el temporizador al llamar Happy()
    }
    public void Victory()
    {
        animator.SetBool("Victory", true);

        isTheEnd = true; // Activar la bandera
        _timer = 0f; // Reiniciar el temporizador al llamar Happy()
    }
    public void Defeat()
    {
        animator.SetBool("Defeat", true);

        isTheEnd = true; // Activar la bandera
        _timer = 0f; // Reiniciar el temporizador al llamar Happy()
    }
}