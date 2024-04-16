using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    InputCharacters input;

    public Vector2 CurrentMovementInput { get; private set; }
    public bool IsRunPressed { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsChangeCameraPressed { get; private set; }
    public bool IsDancePressed { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        input = new InputCharacters();

        //Esto se ejecuta cuando el personaje pulsa AWSD o el left stick del gamepad
        input.Player.Move.started += onMovementInput;
        input.Player.Move.canceled += onMovementInput;
        input.Player.Move.performed += onMovementInput;
        //Esto se ejecuta cuando el personaje pulsa shift Izq o el boton del gamepad
        input.Player.Run.started += onRunInput;
        input.Player.Run.canceled += onRunInput;
        //Esto se ejecuta cuando el personaje pulsa espacio o el boton del gamepad
        input.Player.Jump.started += onJumpInput;
        input.Player.Jump.canceled += onJumpInput;
        //Esto se ejecuta cuando el personaje pulsa C o el boton de la izquierda del dpad
        input.Player.ChangeCamera.started += onChangeCameraInput;
        input.Player.ChangeCamera.canceled += onChangeCameraInput;

        input.Player.Dance.started += onDanceInput;
        input.Player.Dance.canceled += onDanceInput;

    }

    private void onDanceInput(InputAction.CallbackContext context)
    {
        IsDancePressed = context.ReadValueAsButton();
    }

    private void onChangeCameraInput(InputAction.CallbackContext context)
    {
        IsChangeCameraPressed = context.ReadValueAsButton();
    }

    private void onJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }

    private void onRunInput(InputAction.CallbackContext context)
    {
        IsRunPressed = context.ReadValueAsButton();
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        //Al detectar los inputs del teclado o gamepad se tendran dos valores de x e y
        CurrentMovementInput = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
