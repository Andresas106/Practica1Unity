using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door2 : MonoBehaviour
{
    public Animator thedoor2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            int keyAmount = other.gameObject.GetComponent<key>().KeyAmount;


            if (keyAmount == 1)
            {
                thedoor2.Play("open_door2");
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            int keyAmount = other.gameObject.GetComponent<key>().KeyAmount;
            if (keyAmount == 1)
            {
                thedoor2.Play("close_door2");
            }
        }

    }

}
