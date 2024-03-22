using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    InputManager inputManager;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float RotationFactorPerFrame = 2.5f;

    int isWalkingHash;
    int isRunningHash;
    
    //variables de gravedad
    float groundedGravity = -0.05f;
    float gravity;

    //variables de salto
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 3.0f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpingAnimating = false;

    public Camera mainCamera;

    //variables modificació moviment en aigua
    //variables esfera que chequeja si està tocant el terra
    public float groundSphereRadius=0.1f;
    public Transform GroundChecker;
    //multiplicador que sloweja al jugador quan camina per aigua
    public float movementSpeedMultiplier = 1f;
    //ens permet assignar la variable whatiswater a les capes que vulguem
    //(en aques cas a la capa "Water", que l'assignem a totes les superfícies d'aigua)
    public LayerMask WhatIsWater;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        setUpJumpVariables();
    }

    private void setUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    // Update is called once per frame
    void Update()
    {
        IsWatered();
        WateredEffect();

        handleMovement();
        handleRotation();
        

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        handleGravity();
        handleJump();
       

      
    }

    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpingAnimating = true;
            isJumping = true;
            currentMovement.y = initialJumpVelocity * .5f;
            currentRunMovement.y = initialJumpVelocity * .5f;
        }
        else if(isJumping && !isJumpPressed && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;
        //aplicar gravedad al personaje si esta en el aire
        if(characterController.isGrounded)
        {
            if (isJumpingAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpingAnimating = false;
            } 

            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        //Para cambiar la posicion de nuestro personaje
        // positionToLookAt.x = currentMovement.x;
        // positionToLookAt.z = currentMovement.z;
        // positionToLookAt.y = 0.0f;

        positionToLookAt = currentMovement.normalized;
        positionToLookAt.y = 0.0f;

        //Para cambiar la rotacion de nuestro personaje
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //crear nueva rotacion basada en donde el personaje esta pulsando
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, RotationFactorPerFrame* Time.deltaTime);
        }
    }

    void handleMovement()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        currentMovementInput = inputManager.CurrentMovementInput;
        isRunPressed = inputManager.IsRunPressed;
        isJumpPressed = inputManager.IsJumpPressed;

        if(currentMovementInput.x != 0 || currentMovementInput.y != 0)
        {
            isMovementPressed = true;
        }
        else
        {
            isMovementPressed = false;
        }

        //Empezar a caminar si se presiona la tecla
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        //dejar de caminar
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        //empezar a correr si se presiona la tecla
        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        //dejar de correr
        else if((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        Quaternion cameraRotation = mainCamera.transform.rotation;

        Vector3 moveDirection = cameraRotation * new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y);

        currentMovement.x = moveDirection.x * 3.0f * movementSpeedMultiplier;
        currentMovement.z = moveDirection.z * 3.0f * movementSpeedMultiplier;

        currentRunMovement.x = moveDirection.x * 6.0f * movementSpeedMultiplier;
        currentRunMovement.z = moveDirection.z * 6.0f * movementSpeedMultiplier;


    }

    //modificació moviment en aigua

    //fem que ckequeji si el jugador està sobre una superfície d'aigua
    private bool IsWatered()
    {
        return Physics.CheckSphere(GroundChecker.position, groundSphereRadius, WhatIsWater);
    }
    //apliquem l'efecte de l'aigua
    void WateredEffect()
    {
        //si està en aigua multipliquem la velocitat per 0.5
        if (IsWatered())
        {
            movementSpeedMultiplier = 0.5f;
        }

        //si no la tornem al valor 1
        else
        {
            movementSpeedMultiplier = 1f;
        }
    }
   

}
