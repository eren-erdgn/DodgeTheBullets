using System;
using EventSystem;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour {
        
        private Camera _playerCamera;
        private Vector3 _rayOrigin;
        private Vector3 _rayDirection;
        private RaycastHit _hit;
        private Vector3 _rayToNormal;
        private Vector3 _hitPos;
        [SerializeField] private float lifetime = 3f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float skinWidth = .1f;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
        }

        

        private void Update () {
            var moveDistance = speed * Time.deltaTime;
            CheckCollisions (moveDistance);
            transform.Translate (Vector3.forward* moveDistance);
        }


        private void CheckCollisions(float moveDistance) {
            var transform1 = transform;
            var ray = new Ray (transform1.position, transform1.forward);

            if (Physics.Raycast(ray, out var hit, moveDistance + skinWidth)) {
                OnHitObject(hit.collider);
            }
        }

        private void OnHitObject(Collider c) {
            if ( c.gameObject.CompareTag("TurretBullet"))
            {
                Events.OnBulletCountUp?.Invoke();
                Destroy(c.gameObject);
                
            }
        }
    }
}