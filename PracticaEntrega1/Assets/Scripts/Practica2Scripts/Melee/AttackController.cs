using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController: MonoBehaviour
{

    InputManager inputManager;

    private bool isAttackPressed;// Variable para saber si el bot�n de ataque est� presionado
    private bool isRunPressed;// Variable para saber si el bot�n de correr est� presionado
    private Vector2 walkingInput;// Variable para capturar la direcci�n del movimiento
    private bool isWalkingPressed;// Variable para saber si se est� caminando
    private bool isJumpPressed;// Variable para saber si el bot�n de salto est� presionado
    private bool isDancePressed;// Variable para saber si el bot�n de bailar est� presionado
    private Animator animator;// Referencia al componente Animator para controlar animaciones
    private bool onGoing = false;// Estado para evitar ataques repetidos en un corto per�odo de tiempo

    public GameObject gun;// Referencia para saber si el arma est� activa
    public GameObject triggerLeg;// Referencia al contacto del ataque

   
    private SoundManager soundManager; // Al controlador de sonidos

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();// Encuentra la referencia al SoundManager para reproducir sonidos
    }

   
    void Start()
    {
        // Inicializa referencias a los componentes de entrada y animaci�n
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        GetInputsCtrl();

        // Comprueba si el arma no est� activa y se ha presionado el bot�n de ataque
        if (!gun.activeInHierarchy)
        {
            if (isAttackPressed && !onGoing)
            {
                // Si se presiona el ataque y no est� en progreso lo inicia
                onGoing = true; // Marcar el ataque como en progreso
                animator.SetBool("isAttacking", true);// Activa la animaci�n de ataque
                triggerLeg.SetActive(true);
                StartCoroutine(wait());
                soundManager.SeleccionAudio(1, 0.3f);// Reproduce un sonido de ataquede patada

            }
            else if ((isWalkingPressed || isRunPressed || isJumpPressed || isDancePressed) && !isAttackPressed)
            {
                // Si hay otras acciones en progreso y no se presiona el ataque, desactiva el ataque
                animator.SetBool("isAttacking", false);// Desactiva la animaci�n de ataque
                triggerLeg.SetActive(false);// Desactiva el objeto de ataque
            }
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);// Espera un segundo

        animator.SetBool("isAttacking", false);// Desactiva la animaci�n de ataque
        triggerLeg.SetActive(false);// Desactiva el objeto de ataque
        onGoing = false;// El ataque ya no est� en progreso
    }

    private void GetInputsCtrl()
    {
        // Obtiene los estados de las entradas
        isAttackPressed = inputManager.IsAttackPressed;// El bot�n de ataque
        isRunPressed = inputManager.IsRunPressed;// El bot�n de correr
        isJumpPressed = inputManager.IsJumpPressed;// El bot�n de salto
        walkingInput = inputManager.CurrentMovementInput;// Direcci�n de movimiento
        isDancePressed = inputManager.IsDancePressed;// El bot�n de bailar

        // Comprueba si hay movimiento para determinar si se est� caminando
        if (walkingInput.x != 0 || walkingInput.y != 0)
        {
            isWalkingPressed = true;
        }
        else
        {
            isWalkingPressed = false;
        }
    }


}