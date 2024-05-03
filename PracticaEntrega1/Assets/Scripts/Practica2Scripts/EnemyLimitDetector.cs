using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLimitDetector : MonoBehaviour
{
    public Transform CheckPoint;
    private float Speed = 2;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsWall;
    public Transform Teletransport;
    private float timeSinceLastGround = 0f;
   

    // Referencia al componente Animator
    private Animator animator;

    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsPatrolling"))
        {
            if (NoGround() || HitWall())
            {
                timeSinceLastGround += Time.deltaTime;
                if (timeSinceLastGround >= 0.1f)
                {
                    // Teletransportar al punto específico
                    transform.position = Teletransport.position;
                    timeSinceLastGround = 0f; // Reiniciar el temporizador después de teletransportarse
                }
                else
                {
                    // Girar solo si no ha pasado suficiente tiempo para teletransportarse
                    Turn(); 
                }
            }
            else
            {
                Move();
            }
        }
    }

    private bool NoGround()
    {
        // Revisar si hay suelo debajo o si hay algún objeto (como una pared) frente al enemigo
        return !Physics.Raycast(CheckPoint.position, Vector3.down, 0.55f, WhatIsGround) || Physics.Raycast(transform.position, transform.forward, 1f, WhatIsGround);
    }

    private bool HitWall()
    {
        // Revisar si hay una pared frente al enemigo
        return Physics.Raycast(transform.position, transform.forward, 1f, WhatIsWall);
    }

    private void Turn()
    {
        // Girar aleatoriamente
        float angle = Random.Range(90f, 270f);
        transform.Rotate(new Vector3(0, angle, 0));
    }

    private void Move()
    {
        // Mover hacia adelante
        if(!animator.GetBool("IsAttacking") && !animator.GetBool("IsOnRange")) transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
