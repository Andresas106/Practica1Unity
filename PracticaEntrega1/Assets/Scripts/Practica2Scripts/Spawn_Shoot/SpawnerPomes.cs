using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParametre : MonoBehaviour
{
    public GameObject poma;
    public float spawnInterval = 5f;
    float timer;
    // Start is called before the first frame update
    void Update()
    {
        spawnPoma();
    }
    void spawnPoma()
    {

        // Incrementamos el temporizador con el tiempo transcurrido desde el último frame
        timer += Time.deltaTime;

        // Si el temporizador supera el intervalo de tiempo deseado
        if (timer >= spawnInterval)
        {
            //fem q espawneji la variable cub que hem creat en la posició de l'spawner
            //(un empty on hem posat l'script) i la rotació la posem nul·la
            //(objecte a spawnejar(GameObject), posició a espawnejar(vector3), rotació)
            Instantiate(poma, transform.position, Quaternion.identity);
            //reiniciem el temporitzador
            timer = 0f;
        }
    }
}
