using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Enemydamage = 10, Playerdamage = 20;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        var go = other.GetComponent<ITakeDamage>();


        if(go != null)
        {
            if (player.CompareTag("player"))
            {
                if (other.tag == "Enemigo")
                {
                    go.TakeDamage(Playerdamage, true);
                }
            }
            else if (player.CompareTag("Enemigo"))
            {
                if (other.tag == "player")
                {
                    go.TakeDamage(Enemydamage, false);
                }
            }
        }
    }
}
