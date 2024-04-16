using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavour : StateMachineBehaviour
{
    //Calcular la distanci necesiutamos la posicion
    Transform _player;
    float _timer;
    private float Speed = 2;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        //bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerClose = CheckPlayer(animator.transform);
        animator.SetBool("IsChasing", isPlayerClose);

        //Do Stuff
        Move(animator.transform);
    }
    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
        mySelf.Translate(mySelf.forward * Speed * Time.deltaTime, Space.Self);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 4;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
