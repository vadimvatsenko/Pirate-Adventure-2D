using System.Collections.Generic;
using Components;
using Components.Spawn;
using Creatures.CreatureVFX;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
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

        protected float MoveSpawnTimer = 0.25f;
        
        protected virtual void Awake()
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

        protected virtual void OnEnable()
        {
            _creature.SubscribeOnJumpEvent(HandleSpawnJumpParticle);
            _creature.SubscribeOnAttackEvent(HandleSpawnAttack1Particle);
            _creature.SubscribeOnDeathEvent(HandleSpawnBloodParticle);
            
        }
        protected virtual void OnDisable()
        {
            _creature.UnsubscribeOnJumpEvent(HandleSpawnJumpParticle);
            _creature.UnsubscribeOnAttackEvent(HandleSpawnAttack1Particle);
            _creature.UnsubscribeOnDeathEvent(HandleSpawnBloodParticle);
        }

        private void Update()
        {
            HandleSpawnFallPartical();
            HandleSpawnMovementPartical();

            HandleTimer();
        }
        
        protected virtual void HandleSpawnMovementPartical()
        {
            if (_creature.XInput != 0 && MoveSpawnTimer <= 0 && _collisionInfo.IsGrounded)
            {
                HandleSpawn(ParticleType.Move);
                MoveSpawnTimer = 0.25f;
            }
        }

        private void HandleTimer()
        {
            MoveSpawnTimer -= Time.deltaTime;
        }
        
        public void HandleSpawnFallPartical()
        {
            float vel = Mathf.Abs(_creature.Rb2D.velocity.y);
            
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

        protected void HandleSpawn(ParticleType type)
        {
            if (_particleMap.TryGetValue(type, out SpawnComponent particle))
            {
                particle.Spawn();
            }
        }
        private void HandleSpawnJumpParticle() => HandleSpawn(ParticleType.Jump);
        private void HandleSpawnAttack1Particle() => HandleSpawn(ParticleType.Attack1);
        private void HandleSpawnBloodParticle() => HandleSpawn(ParticleType.Blood);

    }
}