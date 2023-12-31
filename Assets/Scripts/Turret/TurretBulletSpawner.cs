using EventSystem;
using UnityEngine;

namespace Turret
{
    public class TurretBulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private TurretBullet bulletPrefab;
        [SerializeField] private TurretRandomBullet randomBulletPrefab;
        private Transform _playerPos;
        private Vector3 _playerNormalizedPos;

        private bool _isRandomModeOn;
    
        private float _timer;
        [SerializeField]private float spawnDelay= 3f;
        [SerializeField]private float randomSpawnDelay= 1f;

        private void OnEnable()
        {
            Events.OnPlayerInRange.AddListener(RandomModeTrigger);
        }

        private void OnDisable()
        {
            Events.OnPlayerInRange.RemoveListener(RandomModeTrigger);
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
        private void RandomModeTrigger(bool flag)
        {
            _isRandomModeOn = flag;
        }
    
    
    }
}
