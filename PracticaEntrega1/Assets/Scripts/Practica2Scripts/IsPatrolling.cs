using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IsPatrolling : StateMachineBehaviour
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
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        //Hay que poner que es falso el isTimeUp
        animator.SetBool("IsPatrolling", !isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);

        //Do Stuff
        Move(animator.transform);
    }

    private void Move(Transform mySelf)
    {
        mySelf.Translate(mySelf.forward * Speed * Time.deltaTime);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 6;
    }

    private bool CheckTime()
    {
        //Incrementar el timer amb el temps que ha pasat desde l'ultim update
        _timer += Time.deltaTime;
        return _timer > 4;
    }


}
