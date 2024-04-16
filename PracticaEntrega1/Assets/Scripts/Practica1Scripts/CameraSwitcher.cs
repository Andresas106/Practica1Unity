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

        Cursor.visible = false;
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

            // Rotar camara de primera persona hacia donde esta mirando el personaje
            RotateCamera();

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

    void RotateCamera()
    {
        // Calcular la dirección hacia la que mira el personaje
        Vector3 lookDirection = playerModel.transform.forward;

        // Orientar el POV de la cámara en primera persona hacia la dirección de mira del personaje
        CinemachinePOV pov = firstPersonCamera.GetCinemachineComponent<CinemachinePOV>();
        if (pov != null)
        {
            pov.m_HorizontalAxis.Value = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
            pov.m_VerticalAxis.Value = Mathf.Asin(lookDirection.y) * Mathf.Rad2Deg;
        }
    }


}
