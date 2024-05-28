using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Animator animator;
    float _timer;
    // Boleana para verificar si se ha llegado al final
    bool isTheEnd; 

    void Start()
    {
        animator = GetComponent<Animator>();
        // Inicializar _timer
        _timer = 0f; 
        // Inicializar isTheEnd
        isTheEnd = false; 
    }

    void Update()
    {
        if (isTheEnd)
        {
            // Incrementar _timer 
            _timer += Time.deltaTime; 

            if (_timer > 4)
            {
                animator.SetBool("Happy", false);
                animator.SetBool("Angry", false);
                animator.SetBool("Victory", false);
                animator.SetBool("Defeat", false);
                // Resetear isTheEnd
                isTheEnd = false;
                // Reiniciar el temporizador
                _timer = 0f; 
            }
        }
    }

    public void Happy()
    {
        animator.SetBool("Happy", true);
        // Activar
        isTheEnd = true;
        // Reiniciar el temporizador al llamar Happy()
        _timer = 0f; 
    }
    public void Angry()
    {
        animator.SetBool("Angry", true);
        // Activar
        isTheEnd = true;
        // Reiniciar el temporizador al llamar Angry()
        _timer = 0f; 
    }
    public void Victory()
    {
        animator.SetBool("Victory", true);
        // Activar 
        isTheEnd = true;
        // Reiniciar el temporizador al llamar Victory()
        _timer = 0f; 
    }
    public void Defeat()
    {
        animator.SetBool("Defeat", true);
        // Activar 
        isTheEnd = true;
        // Reiniciar el temporizador al llamar Defeat()
        _timer = 0f; 
    }
}