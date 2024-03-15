using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public int KeyAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            KeyAmount += 1;
            Destroy(other.gameObject);
        }
    }
}
