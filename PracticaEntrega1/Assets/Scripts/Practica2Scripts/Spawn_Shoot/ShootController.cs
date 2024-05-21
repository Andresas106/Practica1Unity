using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public LineRenderer laserLine;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 100f;
    public float laserDuration = 0.1f;
    public GunDamageDealer damageDealer; // Cambiado de DamageDealer a GunDamageDealer
    public Transform shootingPosition; // La posición desde la que se dispara
    public Texture2D laserTexture; // Textura del rayo láser
    public GameObject hitVFXPrefab; // Prefab del VFX que se generará en el impacto

    private InputManager inputManager;
    private bool AttackPressed;
    private Transform gunTransform; // Referencia al transform de la pistola
    private bool once = false;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;

        // Obtener la referencia al transform de la pistola
        gunTransform = transform.GetChild(0); // Asume que la pistola es el primer hijo del jugador
    }

    void Update()
    {
        AttackPressed = inputManager.IsAttackPressed;

        if (AttackPressed)
        {
            // Disparar solo si el botón de disparo está presionado
            Disparar();
            soundManager.SeleccionAudio(2, 0.5f);
        }
    }

    void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootingPosition.position, shootingPosition.forward, out hit, laserMaxLength))
        {
            if (hit.collider.CompareTag("Enemigo") && !once)
            {
                once = true;
                damageDealer.CauseDamage(hit.collider.gameObject, "player");
                StartCoroutine(ShowLaser(shootingPosition.position, hit.point));
                Instantiate(hitVFXPrefab, hit.point, Quaternion.identity); // Instanciar VFX en la posición de impacto
            }
            else
            {
                StartCoroutine(ShowLaser(shootingPosition.position, hit.point));
                Instantiate(hitVFXPrefab, hit.point, Quaternion.identity); // Instanciar VFX en la posición de impacto
            }
        }
        else
        {
            Vector3 endPosition = shootingPosition.position + shootingPosition.forward * laserMaxLength;
            StartCoroutine(ShowLaser(shootingPosition.position, endPosition));
        }
    }

    IEnumerator ShowLaser(Vector3 startPosition, Vector3 endPosition)
    {
        laserLine.enabled = true;
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;
        laserLine.material.mainTexture = laserTexture;
        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition(1, endPosition);
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
        once = false;
    }
}

