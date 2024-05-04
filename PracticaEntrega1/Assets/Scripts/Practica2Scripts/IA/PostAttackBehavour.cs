using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PostAttackBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    //Referencia al script HealthBar
    HealthBar _healthBar;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Detección del jugador por tag
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;
        // Obtiene la referencia al script HealthBar del objeto del animator
        _healthBar = animator.GetComponent<HealthBar>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerInjured = CheckHealth(animator.transform);
        //Se activan o no las boleanas si se corresponden o no con lo que se retorna de las funciones
        animator.SetBool("IsOnRange", !isPlayerClose);
        animator.SetBool("IsAttacking", isPlayerClose);
        animator.SetBool("IsOnRangeInjured", isPlayerInjured);


        //Mirar al jugador
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return (_healthBar.health > (_healthBar.maxHealth * 0.5f)) && distance < 3;
    }
    //Se comprueba el paso del tiempo i se incrementa el timer desde el último update
    private bool CheckTime()
    {
        //Incrementar el timer amb el temps que ha pasat desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 4;
    }

    private bool CheckHealth(Transform mySelf)
    {
        // Compara la salud actual con la salud máxima. Devuelve tanto la salud como la distancia con respecto al jugador
        if (_healthBar != null)
        {
            float distance = Vector3.Distance(_player.position, mySelf.position);
            return _healthBar.health <= _healthBar.maxHealth * 0.5f && distance < 3;
        }
        return false;
    }
}
