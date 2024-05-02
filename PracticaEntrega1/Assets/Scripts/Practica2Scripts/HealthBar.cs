using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour, ITakeDamage, IHealDamage
{
    public GameObject player;
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health) healthSlider.value = health;


        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value,  health, lerpSpeed);
        }

        
    }

    public void TakeDamage(float amount, bool isEnemy)
    {
        health -= amount;
        if (health <= 0 && !isEnemy)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else if(health <= 0)
        {
            Destroy(player);
        }
        
    }

    public void HealDamage(float amount)
    {
        if(health != maxHealth) health += amount;
    }
}
