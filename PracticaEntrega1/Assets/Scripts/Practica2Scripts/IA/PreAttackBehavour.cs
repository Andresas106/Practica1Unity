using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PreAttackBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    //Referencia al script HealthBar
    HealthBar _healthBar;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Detecci�n del jugador por tag
        _player = GameObject.FindGameObjectWithTag("player").transform;
        //Inicializaci�n del timer
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
        animator.SetBool("IsAttacking", isTimeUp);
        animator.SetBool("IsOnRange", isPlayerClose);
        animator.SetBool("IsOnRangeInjured", isPlayerInjured);


        //Determina el punto hacia el que mira el personaje (en este caso al jugador)
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
    }
    //Se comprueba el estado de salud del NPC y su distancia con respecto al jugador
    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return (_healthBar.health > (_healthBar.maxHealth * 0.5f)) && distance < 6;
    }
    //Se comprueba el paso del tiempo i se incrementa el timer desde el �ltimo update
    private bool CheckTime()
    {
        _timer += Time.deltaTime;
        return _timer > 2;
    }

    private bool CheckHealth(Transform mySelf)
    {
        // Compara la salud actual con la salud m�xima. Devuelve tanto la salud como la distancia con respecto al jugador
        if (_healthBar != null)
        {
            float distance = Vector3.Distance(_player.position, mySelf.position);
            return (_healthBar.health <= (_healthBar.maxHealth * 0.5f)) && distance < 6;
        }
        return false;
    }

}
