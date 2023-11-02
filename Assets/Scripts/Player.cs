using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [Range(100,200)]
    [SerializeField] private int maxHealth = 100;
    
    private Transform _playerPosition;
    
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform bulletSpawn;
    //[SerializeField] private float bulletSpeed = 6f;
    
    public static event Action OnPlayerHealthChanged;
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
