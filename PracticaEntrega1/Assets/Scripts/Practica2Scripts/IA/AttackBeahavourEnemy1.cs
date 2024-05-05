using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class AttackBehavourEnemy1 : StateMachineBehaviour
{
    Transform _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("player").transform;
        
        // Se inicia el ataque mirando a un punto random
        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));
        animator.transform.LookAt(rdmPointInPlane);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isPlayerClose = CheckPlayer(animator.transform);
        animator.SetBool("IsOnRange", isPlayerClose);
        //El enemigo mira al jugador constantemente mientras lo ataca
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.LookAt(_player);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 3;
    }

    

}
