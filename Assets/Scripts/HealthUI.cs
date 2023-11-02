using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthUI : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI healthText;
    public Image healthBar;
    private float _lerpSpeed = 10f;

    private void OnEnable()
    {
        Player.OnPlayerHealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
        Player.OnPlayerHealthChanged -= DisplayHealth;
    }

    

    private void DisplayHealth()
    {
        healthText.text = player.Health.ToString();
        healthBar.fillAmount = player.Health / 100f;
        Color healthColor = Color.Lerp(Color.red, Color.green, player.Health / 100f);
        healthBar.color = healthColor;
    }
    

    
}
