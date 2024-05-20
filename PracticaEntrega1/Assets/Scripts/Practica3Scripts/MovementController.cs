using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

      
    }

    public void Happy()
    {
        animator.SetBool("Happy", true);
      
    }
}
