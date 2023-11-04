using System;
using Turret;
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
    
        public static event Action OnPlayerHealthChanged;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            
            OnPlayerHealthChanged?.Invoke();
        }

        private void OnEnable()
        {
        
            TurretDetection.OnPlayerInRange += HealTaken;
        }

        private void OnDisable()
        {
            TurretDetection.OnPlayerInRange -= HealTaken;
        }

        private void HealTaken()
        {
            HealTaken(10);
        }
    
        public int Health
        {
            get => health;
            private set
            {
                health = value;
                health = Mathf.Clamp(health, 0, maxHealth);
                OnPlayerHealthChanged?.Invoke();
            }
        }

    
        private void DamageTaken(int damage)
        { 
            Health = health - damage;
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
