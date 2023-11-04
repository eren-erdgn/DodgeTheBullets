using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Transform _playerTransform;
    private Vector3 _playerPos;
    private Vector3 _bulletPos;
    private Vector3 _normalizedDistanceVector;
    
    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _playerPos = _playerTransform.position;
        _bulletPos = transform.position;
        _normalizedDistanceVector = (_playerPos - _bulletPos).normalized;
        
    }

    private void Update()
    {
        transform.Translate(_normalizedDistanceVector * (bulletSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
        Vector3 normal = other.GetContact(0).normal;
        Vector3 reflectedDirection = Vector3.Reflect(_normalizedDistanceVector, normal);
        _normalizedDistanceVector = reflectedDirection;
    }
}