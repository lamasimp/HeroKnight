using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamageable;
    public TMP_Text healthBarText;
    public Slider healthSlider;
    float delayTime = 2f;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("no player found in the scene");
        }
        playerDamageable = player.GetComponent<Damageable>();
       
    }
    // Start is called before the first frame update
    void Start()
    {    
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "Health " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;     
    }
    
    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "Health " + newHealth + "/" + maxHealth;

    }
    private void Update()
    {
        
            
        if(healthSlider.value <= 0)
        {
            StartCoroutine(GameOverWithDelay());
        }
    }
    IEnumerator GameOverWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // Đợi trong 3 giây

        // Sau khi đợi, chuyển đến màn hình Game Over
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}
