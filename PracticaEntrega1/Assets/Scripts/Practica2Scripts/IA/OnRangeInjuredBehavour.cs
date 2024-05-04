using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnRangeInjuredBehavour : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    //Speed inferior porque está herido 
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
        
        animator.SetBool("IsPatrolling", false);
        animator.SetBool("IsOnRangeInjured", isPlayerClose);
        animator.SetBool("IsChasingInjured", isPlayerFar);

        //Do Stuff
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        // Calcula la dirección hacia la que el enemigo debe moverse
        Vector3 directionToPlayer = (_player.position - mySelf.position).normalized;

        // Calcula la rotación para que el enemigo mire hacia la dirección del jugador
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Aplica la rotación solo al eje Y (para evitar que el enemigo se incline hacia arriba o hacia abajo)
        lookRotation.x = 0;
        lookRotation.z = 0;

        // Aplica la rotación al enemigo
        mySelf.rotation = Quaternion.Slerp(mySelf.rotation, lookRotation, Time.deltaTime * 5);

        // Mueve al enemigo hacia adelante en la dirección calculada
        mySelf.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    //Detecta que la distancia al jugador sea inferior a 2
    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 2;
    }
    //Detecta que la distancia al jugador sea inferior a 6
    private bool CheckPlayerFar(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 6;
    }

}
