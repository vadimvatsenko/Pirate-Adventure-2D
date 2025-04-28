using System;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace PlayerFolder.PlayerParticles
{
    public class PlayerParticleEvent :MonoBehaviour
    {
        [SerializeField] private ParticleEntry[] particles;
        
        private Dictionary<ParticleType, SpawnComponent> _particleMap;
        
        private Player _player;
        
        // Партикал приземления
        private const float MinJumpHeight = 11.1f;
        private float _currentJumpHeight;

        private float _moveSpawnTimer = 0.25f;
        
        private void Awake()
        { 
            _player = GetComponentInParent<Player>();
            _particleMap = new Dictionary<ParticleType, SpawnComponent>();

            // заполняем словарь партиклами, где ключ это перечисление 
            foreach (var part in particles)
            {
                _particleMap[part.type] = part.component;
            }
        }

        private void OnEnable()
        {
            _player.OnPlayerJump += HandleSpawnJumpParticle;
        }
        private void OnDisable()
        {
            _player.OnPlayerJump -= HandleSpawnJumpParticle;
        }

        private void Update()
        {
            HandleSpawnFallPartical();
            HandleSpawnMovementPartical();
        }
        
        public void HandleSpawnMovementPartical()
        {
            _moveSpawnTimer -= Time.deltaTime;
            
            if (_player.XInput != 0 && _moveSpawnTimer <= 0 && _player.IsGrounded)
            {
                HandleSpawn(ParticleType.Move);
                _moveSpawnTimer = 0.25f;
            }
        }

        
        public void HandleSpawnFallPartical()
        {
            float vel = Mathf.Abs(_player.Rb.velocity.y);
            
            if (vel > MinJumpHeight)
            {
                _currentJumpHeight = vel;
            }
            
            if (_currentJumpHeight > MinJumpHeight && _player.IsGrounded)
            {
                HandleSpawn(ParticleType.Fall);
                _currentJumpHeight = 0;
            }
        }

        private void HandleSpawn(ParticleType type)
        {
            if (_particleMap.TryGetValue(type, out SpawnComponent particle))
            {
                particle.Spawn();
            }
        }
        private void HandleSpawnJumpParticle() => HandleSpawn(ParticleType.Jump);
    }
}