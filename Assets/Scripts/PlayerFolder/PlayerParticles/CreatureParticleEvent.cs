using System;
using System.Collections.Generic;
using Components;
using Creatures;
using UnityEngine;

namespace PlayerFolder.PlayerParticles
{
    public class CreatureParticleEvent :MonoBehaviour
    {
        [SerializeField] private ParticleEntry[] particles;
        
        private Dictionary<ParticleType, SpawnComponent> _particleMap;
        
        private Creature _creature;
        private CreatureCollisionInfo _collisionInfo;
        
        // Партикал приземления
        private const float MinJumpHeight = 11.1f;
        private float _currentJumpHeight;

        private float _moveSpawnTimer = 0.25f;
        
        private void Awake()
        { 
            _creature = GetComponentInParent<Creature>();
            if (_creature != null)
            {
                _collisionInfo = _creature.GetComponent<CreatureCollisionInfo>();
            }
            _particleMap = new Dictionary<ParticleType, SpawnComponent>();

            // заполняем словарь партиклами, где ключ это перечисление 
            foreach (var part in particles)
            {
                _particleMap[part.type] = part.component;
            }
        }

        private void OnEnable()
        {
            _creature.SubscribeOnCreatureJump(HandleSpawnJumpParticle);
            _creature.SubscribeOnCreatureAttack(HandleSpawnAttack1Particle);
        }
        private void OnDisable()
        {
            _creature.UnsubscribeOnCreatureJump(HandleSpawnJumpParticle);
            _creature.UnSubscribeCreatureAttack(HandleSpawnAttack1Particle);
        }

        private void Update()
        {
            HandleSpawnFallPartical();
            HandleSpawnMovementPartical();
        }
        
        public void HandleSpawnMovementPartical()
        {
            _moveSpawnTimer -= Time.deltaTime;
            
            if (_creature.XInput != 0 && _moveSpawnTimer <= 0 && _collisionInfo.IsGrounded)
            {
                HandleSpawn(ParticleType.Move);
                _moveSpawnTimer = 0.25f;
            }
        }

        
        public void HandleSpawnFallPartical()
        {
            float vel = Mathf.Abs(_creature.Rb.velocity.y);
            
            if (vel > MinJumpHeight)
            {
                _currentJumpHeight = vel;
            }
            
            if (_currentJumpHeight > MinJumpHeight && _collisionInfo.IsGrounded)
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
        private void HandleSpawnAttack1Particle() => HandleSpawn(ParticleType.Attack1);
    }
}