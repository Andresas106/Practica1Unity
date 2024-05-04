using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSwordDamage : MonoBehaviour
{
    public GameObject Sword;
    
    // Update is called once per frame
    void Update()
    {
        //Si IsOnRange se activa, el collider de la espada también lo hace
        if(GetComponent<Animator>().GetBool("IsOnRange"))
        {
            Sword.SetActive(true);
        }
        else
        {
            Sword.SetActive(false);
        }
    }
}
