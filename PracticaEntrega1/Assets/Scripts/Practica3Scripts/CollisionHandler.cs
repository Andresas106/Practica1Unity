using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Prefab de la part�cula a instanciar cuando ocurra una colisi�n
    public GameObject collisionParticlePrefab;

    // Efecto de part�culas adicional a instanciar cuando ocurra una colisi�n
    public GameObject additionalParticleEffectPrefab;

    // M�todo que se llama cuando una part�cula colisiona con otro objeto
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[ps.GetSafeCollisionEventSize()];

        // Obtenemos el n�mero de colisiones
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        // Destruir la part�cula original
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].remainingLifetime = 0f;
        }
        ps.SetParticles(particles, particles.Length);

        // Instanciar la part�cula de colisi�n en la posici�n de la colisi�n
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Instantiate(collisionParticlePrefab, collisionEvents[i].intersection, Quaternion.identity);
        }

        // Instanciar el efecto de part�culas adicional
        if (additionalParticleEffectPrefab != null)
        {
            Instantiate(additionalParticleEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}