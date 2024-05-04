using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    // Array que contiene diferentes clips de audio para ser reproducidos
    [SerializeField] private AudioClip[] audios;

    // Componente AudioSource para reproducir sonidos
    private AudioSource controlAudio;

    // M�todo Awake se llama cuando el script es cargado
    private void Awake()
    {
        // Obtiene el componente AudioSource del mismo GameObject
        controlAudio = GetComponent<AudioSource>();
    }

    // M�todo para seleccionar y reproducir un audio espec�fico
    public void SeleccionAudio(int indice, float volumen)
    {
        // Reproduce un �nico sonido del array de clips de audio en un volumen espec�fico
        controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
