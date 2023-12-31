using EventSystem;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        public PlayerActions player;
        public TextMeshProUGUI healthText;
        public Image healthBar;

        private void OnEnable()
        {
            Events.OnDisplayPlayerHealth.AddListener(DisplayHealth);
        }

        private void OnDisable()
        {
            Events.OnDisplayPlayerHealth.RemoveListener(DisplayHealth);
        }

    

        private void DisplayHealth()
        {
            healthText.text = player.Health.ToString();
            healthBar.fillAmount = player.Health / 100f;
            var healthColor = Color.Lerp(Color.red, Color.green, player.Health / 100f);
            healthBar.color = healthColor;
        }
    

    
    }
}
