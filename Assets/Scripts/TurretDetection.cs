using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TurretDetection : MonoBehaviour
{
    
    private LineRenderer _circleRenderer;
    private Transform _explosivePosition;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private int resolution = 10;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float distance;
    private Vector3 _explosivePos;
    private Vector3 _playerPos;
    
    private float _timer = 0f;
    [SerializeField]private float healDelay= 3f;

    
    public static event Action OnPlayerInRange;
    

    private void Awake()
    {
        _circleRenderer = GetComponent<LineRenderer>();
        _explosivePosition = GetComponent<Transform>();
        _circleRenderer.materials[0].color = Color.gray;
        
    }

    private void Start()
    {
        DrawCircle();
    }
    private void Update()
    {
        
        CalculateDistance();
        if (distance < radius)
        {
            _timer += Time.deltaTime;
            if (_timer >= healDelay)
            {
                GiveHealth();
                _timer = 0f;
            }
            _circleRenderer.materials[0].color = Color.green;
        }
        else
        {
            _circleRenderer.materials[0].color = Color.gray;
        }
            
    }
    private void GiveHealth()
    {
        OnPlayerInRange?.Invoke();
    }
    private float CalculateDistance()
    {
        _explosivePos = _explosivePosition.position;
        _playerPos = playerPosition.position;
        distance = Mathf.Sqrt((_playerPos.x - _explosivePos.x)*(_playerPos.x - _explosivePos.x) + (_playerPos.z - _explosivePos.z)*(_playerPos.z - _explosivePos.z));
        return distance;
    }
    private void DrawCircle()
    {
        _circleRenderer.loop = true;  
        _circleRenderer.positionCount = resolution;

        var angle = 0f;
        
        for (int i = 0; i < resolution; i++)
        {
            var x = radius * Mathf.Cos(angle);
            var z = radius * Mathf.Sin(angle);

            _circleRenderer.SetPosition(i, new Vector3(x, _explosivePosition.position.y-0.9f, z));

            angle += 2f * Mathf.PI / resolution;
        }
    }
}