using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavour : StateMachineBehaviour
{
    //Calcular la distanci necesiutamos la posicion
    Transform _player;
    float _timer;
    HealthBar _healthBar;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // la variable player busca al jugador
        _player = GameObject.FindGameObjectWithTag("player").transform;
        //Se inicializa la variable _timer al inicializarse 
        _timer = 0;
        _healthBar = animator.GetComponent<HealthBar>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerInjured = CheckHealth(animator.transform);
        bool paramExists = ContainsParam(animator, "IsChasingInjured");


        animator.SetBool("IsChasing", isPlayerClose);
        animator.SetBool("IsPatrolling", isTimeUp);
        if(paramExists)
        {
            animator.SetBool("IsChasingInjured", isPlayerInjured);
        }
        
      
        
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return (_healthBar.health > (_healthBar.maxHealth * 0.5f)) && distance < 6;
    }

    private bool CheckTime()
    {
        //Incrementar el timer amb el temps que ha pasat desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 2;
    }

    private bool CheckHealth(Transform mySelf)
    {
        // Compara la salud actual con la salud máxima
        if (_healthBar != null)
        {
            float distance = Vector3.Distance(_player.position, mySelf.position);
            return (_healthBar.health <= (_healthBar.maxHealth * 0.5f)) && distance < 6;
        }
        return false;
    }

    public  bool ContainsParam(Animator _Anim, string _ParamName)
    {
        foreach (AnimatorControllerParameter param in _Anim.parameters)
        {
            if (param.name == _ParamName) return true;
        }
        return false;
    }
}
