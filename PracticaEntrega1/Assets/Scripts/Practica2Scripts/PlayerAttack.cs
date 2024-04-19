using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.0f;
    public float attackCooldown = 1.0f;
    public int Damage = 10;

    public float lastAttackTime = 0.0f;

    public string playerTag = "player";
    private GameObject playerObject;

    public int AttackNumber = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        playerObject = GameObject.FindGameObjectWithTag("Player2");
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isPlayerClose = CheckPlayer2(animator.transform);
        animator.SetBool("IsPlayerClose", isPlayerClose);
        bool isReachable = CheckPlayer3(animator.transform);
        animator.SetBool("IsAttacking", isReachable);

        if (isPlayerClose & isReachable)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player2"); // Find player object (consider alternatives)
            if (Time.time - lastAttackTime >= attackCooldown) // Check cooldown
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Update last attack time
            }
        }
    }
    private void AttackPlayer()
    {
        if (playerObject != null)
        {
            Debug.Log("Attacks" + AttackNumber);
            playerObject.GetComponent<PlayerHealthSystem>().PlayerTakesDamage(Damage);

            AttackNumber++;
        }
        else
        {
            Debug.Log("Player doesn't exist");
        }
    }
}
