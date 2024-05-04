using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolBehavour : StateMachineBehaviour
{
    Transform _player;
    //Referencia al script HealthBar
    HealthBar _healthBar; 
    float _timer;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Detección del jugador por tag
        _player = GameObject.FindGameObjectWithTag("player").transform;
        //Inicialización del timer
        _timer = 0;

        //El enemigo empieza a patrullar mirando hacia un punto aleatorio
        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));
        animator.transform.LookAt(rdmPointInPlane);

        // Obtiene la referencia al script HealthBar del objeto del animator
        _healthBar = animator.GetComponent<HealthBar>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);
        bool isPlayerInjured = CheckHealth(animator.transform);
        //Se activan o no las boleanas si se corresponden o no con lo que se retorna de las funciones
        animator.SetBool("IsPatrolling", !isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);
        animator.SetBool("IsChasingInjured", isPlayerInjured);
    }
    //Se comprueba el estado de salud del NPC y su distancia con respecto al jugador
    private bool CheckPlayer(Transform mySelf)
    {
        float distance = Vector3.Distance(_player.position, mySelf.position);
        return _healthBar.health > _healthBar.maxHealth * 0.5f && distance < 6;
    }
    //Se comprueba el paso del tiempo i se incrementa el timer desde el último update
    private bool CheckTime()
    {
        _timer += Time.deltaTime;
        return _timer > 4;
    }

    private bool CheckHealth(Transform mySelf)
    {
        // Compara la salud actual con la salud máxima. Devuelve tanto la salud como la distancia con respecto al jugador
        if (_healthBar != null)
        {
            float distance = Vector3.Distance(_player.position, mySelf.position);
            return _healthBar.health <= _healthBar.maxHealth * 0.5f && distance < 6;
        }
        return false;
    }
}
