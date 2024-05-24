using UnityEngine;

public class Drogado : MonoBehaviour
{
    public GameObject objetoActivo;
    public GameObject objetoInactivo;
    public float duracionActivo = 5f;

    private bool interaccionActivada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!interaccionActivada && other.CompareTag("player"))
        {
            // Ocultar este objeto.
            gameObject.SetActive(false);

            // Alternar la activación de los GameObjects.
            objetoActivo.SetActive(false);
            objetoInactivo.SetActive(true);

            // Establecer la bandera de interacción activada.
            interaccionActivada = true;

            // Programar la restauración de los GameObjects.
            Invoke("RestaurarObjetos", duracionActivo);
        }
    }

    private void RestaurarObjetos()
    {
        // Activar este objeto nuevamente.
        gameObject.SetActive(true);

        // Alternar la activación de los GameObjects nuevamente.
        objetoActivo.SetActive(true);
        objetoInactivo.SetActive(false);

        // Restablecer la bandera de interacción activada.
        interaccionActivada = false;
    }
}
