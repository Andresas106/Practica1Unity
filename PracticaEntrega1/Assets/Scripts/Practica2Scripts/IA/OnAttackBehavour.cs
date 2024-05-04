using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnAttackBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;

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
        //Cuando el tiempo termina pasa a IsOnRange
        animator.SetBool("IsOnRange", isTimeUp);
        //Do Stuff
        Move(animator.transform);
    }
    //Se determina que mire todo el rato al jugador el enemigo 
     private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
    }

    private bool CheckTime()
    {
        //Incrementar el timer amb el temps que ha pasat desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 4;
    }
}
