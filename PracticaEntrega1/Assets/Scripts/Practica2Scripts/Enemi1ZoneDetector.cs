using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemi1ZoneDetector : MonoBehaviour
{
    public Transform CheckPoint;
    private float Speed = 2;
    public LayerMask WhatIsGround;
    // Update is called once per frame
    void Update()
    {
        if (NoGround())
            Turn();

        Move();
    }

    private bool NoGround()
    {
        if (Physics.Raycast(CheckPoint.position, Vector3.down, 0.55f, WhatIsGround))
        {
            return false;
        }
        return true;
    }

    private void Turn()
    {
        //Random la opcion de unity
        float angle = Random.Range(90f, 270f);
        transform.Rotate(new Vector3(0, angle, 0));
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
