using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractiveParticles : MonoBehaviour
{
    bool isWalking;
    bool isRunning;
    bool isJumping;

    public Animator animatorPlayer;
    public ParticleSystem particleSystemPlayer;

    void Update()
    {
        // Obtener el estado del jugador desde el Animator
        isWalking = animatorPlayer.GetBool("isWalking");
        isRunning = animatorPlayer.GetBool("isRunning");
        isJumping = animatorPlayer.GetBool("isJumping");

        // Controlar los parámetros del sistema de partículas según el estado del jugador
        if (isWalking && !isRunning && !isJumping)
        {
            SetParticleParameters(10f, 1f, 0, 0,0.3f);
        }
        else if (isWalking && isRunning && !isJumping)
        {
            SetParticleParameters(40f, 3f, 0, 0,0.3f);
        }
        else if (isJumping)
        {
            SetParticleParameters(50f, 5f, 1, 50, 0.1f); // Cambiado el lifetime a 1 segundo
        }
        else
        {
            // Detener las partículas si no hay ningún estado activo
            particleSystemPlayer.Stop();
        }
    }

    // Función para ajustar los parámetros del sistema de partículas
    void SetParticleParameters(float emissionRate, float speed, int burstCount, int burstRate, float lifetime = -1f)
    {
        var emission = particleSystemPlayer.emission;
        emission.rateOverTime = emissionRate;

        var mainModule = particleSystemPlayer.main;
        mainModule.startSpeed = speed;

        if (lifetime != -1f) // Si se proporciona un valor de lifetime específico
        {
            mainModule.startLifetime = lifetime; // Establecer el tiempo de vida inicial de las partículas
        }

        if (burstCount > 0)
        {
            var burst = new ParticleSystem.Burst(0.0f, (short)burstCount, (short)burstRate, 1, 0.0f);
            particleSystemPlayer.emission.SetBurst(0, burst);
        }

        // Iniciar el sistema de partículas si no está reproduciéndose
        if (!particleSystemPlayer.isPlaying)
        {
            particleSystemPlayer.Play();
        }
    }
}