using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Prefab de la partícula a instanciar cuando ocurra una colisión
    public GameObject collisionParticlePrefab;

    // Efecto de partículas adicional a instanciar cuando ocurra una colisión
    public GameObject additionalParticleEffectPrefab;

    // Método que se llama cuando una partícula colisiona con otro objeto
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[ps.GetSafeCollisionEventSize()];

        // Obtenemos el número de colisiones
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        // Destruir la partícula original
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].remainingLifetime = 0f;
        }
        ps.SetParticles(particles, particles.Length);

        // Instanciar la partícula de colisión en la posición de la colisión
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Instantiate(collisionParticlePrefab, collisionEvents[i].intersection, Quaternion.identity);
        }

        // Instanciar el efecto de partículas adicional
        if (additionalParticleEffectPrefab != null)
        {
            Instantiate(additionalParticleEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}