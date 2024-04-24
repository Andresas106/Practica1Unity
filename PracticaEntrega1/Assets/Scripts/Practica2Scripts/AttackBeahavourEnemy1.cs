using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class AttackBehavourEnemy1 : StateMachineBehaviour
{
    Transform _player;
    float _timer;
    private float Speed = 2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;

        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));

        animator.transform.LookAt(rdmPointInPlane);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check Triggers
        bool isPlayerClose = CheckPlayer(animator.transform);
        animator.SetBool("IsAttacking", isPlayerClose);

        //Do Stuff
        // Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.Translate(mySelf.forward * Speed * Time.deltaTime);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 4;
    }

    

}
