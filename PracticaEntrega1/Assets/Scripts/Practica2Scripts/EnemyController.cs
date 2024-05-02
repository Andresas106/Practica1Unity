using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    private Animator animator;

    void Start()
    {
        targetPoint = 0;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        // Verificar si el animator est� en el estado de patrulla
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Patrolling"))
        {
            // Comprueba si el enemigo ha llegado al punto de patrulla actual
            if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
            {
                IncreaseTargetInt();
            }

            // Mueve al enemigo hacia el punto de patrulla actual
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

            // Orienta al enemigo hacia el punto de patrulla actual
            // Obt�n la direcci�n hacia el waypoint
            Vector3 directionToWaypoint = (patrolPoints[targetPoint].position - transform.position).normalized;

            // Calcula la rotaci�n para que el enemigo mire hacia el waypoint
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            // Modifica la rotaci�n para evitar la inclinaci�n vertical
            targetRotation.x = 0;
            targetRotation.z = 0;

            // Aplica la rotaci�n al enemigo
            transform.rotation = targetRotation;
        }
    }

    void IncreaseTargetInt()
    {
        // Incrementa el �ndice del punto de patrulla y rein�cialo si llega al final del array
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
