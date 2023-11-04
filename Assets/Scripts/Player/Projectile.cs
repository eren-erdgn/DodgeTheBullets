using System;
using Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Projectile : MonoBehaviour, IBullet
    {
        private Camera _playerCamera;
        private Ray _ray;
        private Vector3 _rayToNormal;
        [SerializeField] private float speed = 100f;

        private void Awake()
        {
            _playerCamera = Camera.main;
            if (_playerCamera != null) _ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            _rayToNormal = _ray.direction;
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            transform.Translate(_rayToNormal * (speed * Time.deltaTime));
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("TurretBullet"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
