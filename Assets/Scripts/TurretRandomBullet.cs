using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRandomBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 _bulletPos;
    private Vector3 _normalizedDistanceVector;
    private int _randomX;
    private float _bulletY;
    private float _randomZ;
    
    private void Awake()
    {
        _bulletPos = transform.position;
        _bulletY = _bulletPos.y;
        _randomX = UnityEngine.Random.Range(-5,5);
        _randomZ = UnityEngine.Random.Range(-5,5);
        _normalizedDistanceVector = new Vector3(_randomX, _bulletY, _randomZ).normalized;
    }

    // Update is called once per frame
    void Update()
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
