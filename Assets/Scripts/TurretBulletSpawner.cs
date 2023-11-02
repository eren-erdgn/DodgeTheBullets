using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretBulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private TurretBullet bulletPrefab;
    [SerializeField] private TurretRandomBullet randomBulletPrefab;
    private Transform _playerPos;
    private Vector3 _playerNormalizedPos;

    private bool _isRandomModeOn;
    
    private float _timer = 0f;
    [SerializeField]private float spawnDelay= 3f;
    [SerializeField]private float randomSpawnDelay= 1f;

    private void OnEnable()
    {
        TurretDetection.OnPlayerInRange += RandomModeOn;
        TurretDetection.OnPlayerNotInRange += RandomModeOff;
    }

    private void OnDisable()
    {
        TurretDetection.OnPlayerInRange -= RandomModeOn;
        TurretDetection.OnPlayerNotInRange += RandomModeOff;
    }


    private void Update()
    {
        if(_isRandomModeOn)
            SpawnRandomBullet();
        else
            SpawnBullet();
    }

    private void SpawnRandomBullet()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= randomSpawnDelay)) return;
        Instantiate(randomBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        _timer = 0f;
        
    }
    private void SpawnBullet()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= spawnDelay)) return;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        _timer = 0f;
    }
    private void RandomModeOn()
    {
        _isRandomModeOn = true;
    }
    private void RandomModeOff()
    {
        _isRandomModeOn = false;
    }
    
    
}
