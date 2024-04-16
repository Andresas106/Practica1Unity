using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceController : MonoBehaviour
{

    InputManager inputManager;

    private bool isDancePressed;
    private bool isRunPressed;
    private Vector2 walkingInput;
    private bool isWalkingPressed;
    private bool isJumpPressed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputsCtrl();


        if (isDancePressed)
        {
            animator.SetBool("isDancing", true);
        }
        else if(isWalkingPressed || isRunPressed || isJumpPressed)
        {
            animator.SetBool("isDancing", false);
        }
    }

    private void GetInputsCtrl()
    {
        isDancePressed = inputManager.IsDancePressed;
        isRunPressed = inputManager.IsRunPressed;
        isJumpPressed = inputManager.IsJumpPressed;
        walkingInput = inputManager.CurrentMovementInput;

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
