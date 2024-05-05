using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBasic : MonoBehaviour
{
    //creem aquesta variable on posarem el prefab de la illa
    public GameObject illa;
   
    void Start()
    {
        spawnBasic();
    }

    
void spawnBasic()
{
    Instantiate(illa, transform.position, Quaternion.identity);
}

  
}
