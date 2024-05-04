using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController: MonoBehaviour
{

    InputManager inputManager;

    private bool isAttackPressed;// Variable para saber si el botón de ataque está presionado
    private bool isRunPressed;// Variable para saber si el botón de correr está presionado
    private Vector2 walkingInput;// Variable para capturar la dirección del movimiento
    private bool isWalkingPressed;// Variable para saber si se está caminando
    private bool isJumpPressed;// Variable para saber si el botón de salto está presionado
    private bool isDancePressed;// Variable para saber si el botón de bailar está presionado
    private Animator animator;// Referencia al componente Animator para controlar animaciones
    private bool onGoing = false;// Estado para evitar ataques repetidos en un corto período de tiempo

    public GameObject gun;// Referencia para saber si el arma está activa
    public GameObject triggerLeg;// Referencia al contacto del ataque

   
    private SoundManager soundManager; // Al controlador de sonidos

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();// Encuentra la referencia al SoundManager para reproducir sonidos
    }

   
    void Start()
    {
        // Inicializa referencias a los componentes de entrada y animación
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        GetInputsCtrl();

        // Comprueba si el arma no está activa y se ha presionado el botón de ataque
        if (!gun.activeInHierarchy)
        {
            if (isAttackPressed && !onGoing)
            {
                // Si se presiona el ataque y no está en progreso lo inicia
                onGoing = true; // Marcar el ataque como en progreso
                animator.SetBool("isAttacking", true);// Activa la animación de ataque
                triggerLeg.SetActive(true);
                StartCoroutine(wait());
                soundManager.SeleccionAudio(1, 0.3f);// Reproduce un sonido de ataquede patada

            }
            else if ((isWalkingPressed || isRunPressed || isJumpPressed || isDancePressed) && !isAttackPressed)
            {
                // Si hay otras acciones en progreso y no se presiona el ataque, desactiva el ataque
                animator.SetBool("isAttacking", false);// Desactiva la animación de ataque
                triggerLeg.SetActive(false);// Desactiva el objeto de ataque
            }
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);// Espera un segundo

        animator.SetBool("isAttacking", false);// Desactiva la animación de ataque
        triggerLeg.SetActive(false);// Desactiva el objeto de ataque
        onGoing = false;// El ataque ya no está en progreso
    }

    private void GetInputsCtrl()
    {
        // Obtiene los estados de las entradas
        isAttackPressed = inputManager.IsAttackPressed;// El botón de ataque
        isRunPressed = inputManager.IsRunPressed;// El botón de correr
        isJumpPressed = inputManager.IsJumpPressed;// El botón de salto
        walkingInput = inputManager.CurrentMovementInput;// Dirección de movimiento
        isDancePressed = inputManager.IsDancePressed;// El botón de bailar

        // Comprueba si hay movimiento para determinar si se está caminando
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