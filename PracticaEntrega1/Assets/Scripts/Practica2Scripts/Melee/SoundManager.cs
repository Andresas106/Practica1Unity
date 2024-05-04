using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    // Array que contiene diferentes clips de audio para ser reproducidos
    [SerializeField] private AudioClip[] audios;

    // Componente AudioSource para reproducir sonidos
    private AudioSource controlAudio;

    // Método Awake se llama cuando el script es cargado
    private void Awake()
    {
        // Obtiene el componente AudioSource del mismo GameObject
        controlAudio = GetComponent<AudioSource>();
    }

    // Método para seleccionar y reproducir un audio específico
    public void SeleccionAudio(int indice, float volumen)
    {
        // Reproduce un único sonido del array de clips de audio en un volumen específico
        controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
