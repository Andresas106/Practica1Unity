using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public GameObject playerModel; // Referencia al modelo del personaje



    private bool isFirstPersonActive = false;
    private InputManager inputManager;
    private bool isChangeCameraPressed;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        // Configurar prioridades iniciales
        thirdPersonCamera.Priority = 10;
        firstPersonCamera.Priority = 0;
    }

    void Update()
    {
        isChangeCameraPressed = inputManager.IsChangeCameraPressed;

        // Detectar entrada para cambiar de cámara (por ejemplo, una tecla)
        if (isChangeCameraPressed)
        {
            if (isFirstPersonActive)
            {
                isFirstPersonActive = false;
            }
            else {
                isFirstPersonActive = true;
            } 
            // Alternar entre primera y tercera persona
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Cambiar entre primera y tercera persona
        if (isFirstPersonActive)
        {
            // Activar cámara en primera persona y desactivar tercera persona
            thirdPersonCamera.Priority = 0;
            firstPersonCamera.Priority = 10;
        }
        else
        {
            // Activar cámara en tercera persona y desactivar primera persona
            thirdPersonCamera.Priority = 10;
            firstPersonCamera.Priority = 0;
        }
    }
}
