using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnRangeBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    private float Speed = 2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Detección de jugador. 
        _player = GameObject.FindGameObjectWithTag("player").transform;
        //Tiempo en 0
        _timer = 0;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers

        bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerFar = CheckPlayerFar(animator.transform);
        //Hay que poner que es falso el isTimeUp
        animator.SetBool("IsOnRange", isPlayerClose);
        animator.SetBool("IsChasing", isPlayerFar);


        //Do Stuff
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
        mySelf.Translate(mySelf.forward * Speed * Time.deltaTime);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 3;
    }
    private bool CheckPlayerFar(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 6;
    }

}
