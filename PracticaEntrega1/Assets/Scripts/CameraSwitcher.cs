using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public GameObject playerModel; // Referencia al modelo del personaje
    public Camera mainCamera;



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
            thirdPersonCamera.Priority = 0;
            firstPersonCamera.Priority = 1;

            mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("player"));
        }
        else
        {
            // Activar cámara en tercera persona y desactivar primera persona
            thirdPersonCamera.Priority = 1;
            firstPersonCamera.Priority = 0;

            mainCamera.cullingMask |= (1 << LayerMask.NameToLayer("player"));

        }
    }


}
