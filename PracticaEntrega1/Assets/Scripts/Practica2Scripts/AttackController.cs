using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController: MonoBehaviour
{

    InputManager inputManager;

    private bool isAttackPressed;
    private bool isRunPressed;
    private Vector2 walkingInput;
    private bool isWalkingPressed;
    private bool isJumpPressed;
    private bool isDancePressed;
    private Animator animator;

    public GameObject gun;
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


        if(!gun.activeInHierarchy)
        {
            if (isAttackPressed)
            {
                animator.SetBool("isAttacking", true);
                StartCoroutine(wait());

            }
            else if (isWalkingPressed || isRunPressed || isJumpPressed || isDancePressed)
            {
                animator.SetBool("isAttacking", false);
            }
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

        animator.SetBool("isAttacking", false);
    }

    private void GetInputsCtrl()
    {
        isAttackPressed = inputManager.IsAttackPressed;
        isRunPressed = inputManager.IsRunPressed;
        isJumpPressed = inputManager.IsJumpPressed;
        walkingInput = inputManager.CurrentMovementInput;
        isDancePressed = inputManager.IsDancePressed;

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