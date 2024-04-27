using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBasic : MonoBehaviour
{
    //creem aquesta variable on posarem el prefab del cub que hem creat
    public GameObject cub;
    public float spawnInterval = 5f;
    float timer;
   
    void Start()
    {
        spawnBasic();
    }

    
void spawnBasic()
{
    Instantiate(cub, transform.position, Quaternion.identity);
}

  
}
