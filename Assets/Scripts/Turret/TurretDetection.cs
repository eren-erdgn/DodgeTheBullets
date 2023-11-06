using System;
using EventSystem;
using UnityEngine;

namespace Turret
{
    [RequireComponent(typeof(LineRenderer))]
    public class TurretDetection : MonoBehaviour
    {
    
        private LineRenderer _circleRenderer;
        private Transform _turretPosition;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private int resolution = 10;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float distance;
        private Vector3 _turretPos;
        private Vector3 _playerPos;
    
        private float _timer;
        [SerializeField]private int healAmount = 5;
        [SerializeField]private float healDelay= 3f;
        
        
    
        //public static event Action OnPlayerInRange;
        //public static event Action OnPlayerNotInRange;
    

        private void Awake()
        {
            _circleRenderer = GetComponent<LineRenderer>();
            _turretPosition = GetComponent<Transform>();
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
                Events.OnPlayerInRange?.Invoke(false);
                //OnPlayerNotInRange?.Invoke();
                _circleRenderer.materials[0].color = Color.gray;
            }
            
        }
        private void GiveHealth()
        {
            Events.OnPlayerInRange?.Invoke(true);
            Events.OnPlayerGetHealth?.Invoke(healAmount);
            //OnPlayerInRange?.Invoke();
        }
        private void CalculateDistance()
        {
            _turretPos = _turretPosition.position;
            _playerPos = playerPosition.position;
            distance = Mathf.Sqrt((_playerPos.x - _turretPos.x)*(_playerPos.x - _turretPos.x) + (_playerPos.z - _turretPos.z)*(_playerPos.z - _turretPos.z));
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

                _circleRenderer.SetPosition(i, new Vector3(x, _turretPosition.position.y-0.9f, z));

                angle += 2f * Mathf.PI / resolution;
            }
        }
    }
}