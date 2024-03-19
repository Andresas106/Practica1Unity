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
    private bool isChangeCameraPressedThisFrame = false;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        // Configurar prioridades iniciales
        thirdPersonCamera.Priority = 1;
        firstPersonCamera.Priority = 0;
    }

    void LateUpdate()
    {
        isChangeCameraPressed = inputManager.IsChangeCameraPressed;

        // Detectar entrada para cambiar de cámara (por ejemplo, una tecla)
        if (isChangeCameraPressed && !isChangeCameraPressedThisFrame)
        {
            if (isFirstPersonActive)
            {
                
                isFirstPersonActive = false;
            }
            else {
                isFirstPersonActive = true;
            }

            Debug.Log(isFirstPersonActive);
            // Alternar entre primera y tercera persona
            isChangeCameraPressedThisFrame = true;
            SwitchCamera();
        }
        else if(!isChangeCameraPressed)
        {
            isChangeCameraPressedThisFrame = false;
        }
    }

    void SwitchCamera()
    {
        // Cambiar entre primera y tercera persona
        if (isFirstPersonActive)
        {
            // Activar cámara en primera persona y desactivar tercera persona
            SetModelVisibility(false);
            thirdPersonCamera.Priority = 0;
            firstPersonCamera.Priority = 1;
        }
        else
        {
            // Activar cámara en tercera persona y desactivar primera persona
            SetModelVisibility(true);
            thirdPersonCamera.Priority = 1;
            firstPersonCamera.Priority = 0;
        }
    }

    void SetModelVisibility(bool isVisible)
    {
        // Obtener todos los componentes Renderer del modelo del personaje
        Renderer[] renderers = playerModel.GetComponentsInChildren<Renderer>();

        // Iterar a través de los componentes Renderer y activar/desactivar la renderización
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }
}
