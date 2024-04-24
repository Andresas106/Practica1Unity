using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PreAttackBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    private float Speed = 2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);

        //Hay que poner que es falso el isTimeUp
        animator.SetBool("IsAttacking", isTimeUp);
        animator.SetBool("IsOnRange", isPlayerClose);

        //Do Stuff
       Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 8;
    }

    private bool CheckTime()
    {
        //Incrementar el timer amb el temps que ha pasat desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 2;
    }

}
