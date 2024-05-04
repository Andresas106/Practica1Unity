using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IsPatrolling : StateMachineBehaviour
{
    Transform _player;
    float _timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Detección del jugador por tag
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;
        //Inicia el patrolling mirando a un punto random el enemigo
        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));
        animator.transform.LookAt(rdmPointInPlane);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        //Determina la activación de una u otra boleana dependiendo de lo que devuelven las funciones
        animator.SetBool("IsPatrolling", !isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);

    }
    //Distancia con respecto al jugador menor a 6
    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 6;
    }

    private bool CheckTime()
    {
        //Incrementar el timer con el tiempo que ha pasado desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 4;
    }
}
