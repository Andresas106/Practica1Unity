using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolBehavour : StateMachineBehaviour
{
    Transform _player;
    HealthBar _healthBar; // Referencia al script HealthBar
    float _timer;
    private float Speed = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("player").transform;
        _timer = 0;

        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));

        animator.transform.LookAt(rdmPointInPlane);

        // Obtiene la referencia al script HealthBar del objeto del animator
        _healthBar = animator.GetComponent<HealthBar>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerInjured = CheckHealth();

        animator.SetBool("IsPatrolling", !isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);
        animator.SetBool("IsChasingInjured", isPlayerInjured);
    }

    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return distance < 6;
    }

    private bool CheckTime()
    {
        _timer += Time.deltaTime;
        return _timer > 4;
    }

    private bool CheckHealth()
    {
        // Compara la salud actual con la salud máxima
        if (_healthBar != null)
        {
            return _healthBar.health <= _healthBar.maxHealth * 0.5f;
        }
        return false;
    }
}
