using EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [Range(100,200)]
        [SerializeField] private int maxHealth = 100;
    
        private Transform _playerPosition;
        private int count;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            Events.OnDisplayPlayerHealth?.Invoke();
        }

        private void OnEnable()
        {
            Events.OnBulletCountUp.AddListener(countUp);
            Events.OnPlayerGetDamage.AddListener(DamageTaken);
            Events.OnPlayerGetHealth.AddListener(HealTaken);
        }

        private void OnDisable()
        {
            Events.OnBulletCountUp.RemoveListener(countUp);
            Events.OnPlayerGetDamage.RemoveListener(DamageTaken);
            Events.OnPlayerGetHealth.RemoveListener(HealTaken);
        }
        
        private void countUp()
        {
            count++;
            Debug.Log(count);
        }
        
        public int Health
        {
            get => health;
            private set
            {
                health = value;
                health = Mathf.Clamp(health, 0, maxHealth);
                Events.OnDisplayPlayerHealth?.Invoke();
            }
        }

    
        private void DamageTaken(int damage)
        { 
            Health = health - damage;
            Events.OnDisplayPlayerHealth?.Invoke();
            if (health > 0) return;
            Destroy(gameObject);
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        private void HealTaken(int heal)
        {
            Health = health + heal;
        }
    }
}
