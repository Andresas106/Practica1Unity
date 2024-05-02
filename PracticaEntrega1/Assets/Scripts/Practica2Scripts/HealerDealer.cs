using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealerDealer : MonoBehaviour
{
    private float healTimer = 0f;
    private float healInterval = 1f;


    private void OnTriggerStay(Collider other)
    {
        if (healTimer <= 0f)
        {
            var go = other.GetComponent<IHealDamage>();
            if (go != null)
            {
                go.HealDamage(10);
                healTimer = healInterval;
            }
        }
    }

    private void Update()
    {
        healTimer -= Time.deltaTime;
    }
}
