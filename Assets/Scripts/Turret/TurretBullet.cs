using EventSystem;
using Interface;
using UnityEngine;

namespace Turret
{
    public class TurretBullet : MonoBehaviour , IBullet
    {
        [SerializeField] private float bulletSpeed = 10f;
        private Transform _playerTransform;
        private Vector3 _playerPos;
        private Vector3 _bulletPos;
        private Vector3 _normalizedDistanceVector;
        [SerializeField]private int collisionCount = 30;
        [SerializeField] private int damage = 5;
        private int _collisionCountIndex;
    
        private void Awake()
        {
            
            _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
            _playerPos = _playerTransform.position;
            _bulletPos = transform.position;
            _normalizedDistanceVector = (_playerPos - _bulletPos).normalized;
        
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            transform.Translate(_normalizedDistanceVector * (bulletSpeed * Time.deltaTime));
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Events.OnPlayerGetDamage?.Invoke(damage);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Reflective"))
            {
                _collisionCountIndex++;
        
                if(_collisionCountIndex > collisionCount)
                    Destroy(gameObject);
        
                Vector3 normal = other.GetContact(0).normal;
                Vector3 reflectedDirection = Vector3.Reflect(_normalizedDistanceVector, normal);
                _normalizedDistanceVector = reflectedDirection;
                
            }
            
        }
    }
}
