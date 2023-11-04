using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private Projectile projectilePrefab;
        
        private Transform _playerPosition;
        private Vector3 _playerPos;

        private bool _isRandomModeOn;
    
        [SerializeField]private float msBetweenShots =100f;
        private float _nextShotTime;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SpawnBullet();
            }
            
        }

        

        private void SpawnBullet()
        {
            if (!(Time.time > _nextShotTime)) return;
            _nextShotTime = Time.time + msBetweenShots / 1000;
            Instantiate(projectilePrefab, projectileSpawnPoint.position,Quaternion.identity);
        }
    }
    
    
}
