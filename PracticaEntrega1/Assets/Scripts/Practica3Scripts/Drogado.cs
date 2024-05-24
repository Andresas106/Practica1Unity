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

            // Alternar la activaci�n de los GameObjects.
            objetoActivo.SetActive(false);
            objetoInactivo.SetActive(true);

            // Establecer la bandera de interacci�n activada.
            interaccionActivada = true;

            // Programar la restauraci�n de los GameObjects.
            Invoke("RestaurarObjetos", duracionActivo);
        }
    }

    private void RestaurarObjetos()
    {
        // Activar este objeto nuevamente.
        gameObject.SetActive(true);

        // Alternar la activaci�n de los GameObjects nuevamente.
        objetoActivo.SetActive(true);
        objetoInactivo.SetActive(false);

        // Restablecer la bandera de interacci�n activada.
        interaccionActivada = false;
    }
}
