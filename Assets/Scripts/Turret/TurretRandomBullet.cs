using Interface;
using UnityEngine;

namespace Turret
{
    public class TurretRandomBullet : MonoBehaviour , IBullet
    {
        [SerializeField] private float bulletSpeed = 10f;
        private Vector3 _bulletPos;
        private Vector3 _normalizedDistanceVector;
        private int _randomX;
        private float _bulletY;
        private float _randomZ;
        [SerializeField]private int collisionCount = 30;
        private int _collisionCountIndex;
    
    
        private void Awake()
        {
            _bulletPos = transform.position;
            _bulletY = _bulletPos.y;
            _randomX = Random.Range(-5,5);
            _randomZ = Random.Range(-5,5);
            _normalizedDistanceVector = new Vector3(_randomX, _bulletY, _randomZ).normalized;
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
            if(other.gameObject.CompareTag("Player"))
                Destroy(gameObject);
        
            _collisionCountIndex++;
        
            if(_collisionCountIndex > collisionCount)
                Destroy(gameObject);
            Vector3 normal = other.GetContact(0).normal;
            Vector3 reflectedDirection = Vector3.Reflect(_normalizedDistanceVector, normal);
            _normalizedDistanceVector = reflectedDirection;
        }
    
    }
}
