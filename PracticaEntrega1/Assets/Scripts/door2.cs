using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door2 : MonoBehaviour
{
    public Animator thedoor2;
    private key Key;
    private bool firstTime = true;

    void Start()
    {
        Key = GetComponent<key>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(firstTime);
        if(Key.KeyAmount == 1 && !firstTime)
        {
            thedoor2.Play("open_door2");
        }

        if(Key.KeyAmount == 0)
        {
            firstTime = true;
        }
        else
        {
            firstTime = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(Key.KeyAmount == 1)
        {
            thedoor2.Play("close_door2");
        }
        
    }

}
