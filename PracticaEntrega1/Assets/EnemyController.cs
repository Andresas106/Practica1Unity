using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    void Start()
    {
        targetPoint = 0;
    }

    void Update()
    {
        // Comprueba si el enemigo ha llegado al punto de patrulla actual
        if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            IncreaseTargetInt();
        }

        // Mueve al enemigo hacia el punto de patrulla actual
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void IncreaseTargetInt()
    {
        // Incrementa el índice del punto de patrulla y reinícialo si llega al final del array
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
