using System;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesVFX;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyParticleEvents : CreatureParticleEvent
    {
        private Enemy _sharky;

        protected override void Awake()
        {
            base.Awake();
            _sharky = GetComponentInParent<Enemy>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _sharky.SubscribeOnAgroEvent(HandleExclamationParticle);
            _sharky.SubscribeOnWTFEvent(SpawnWtf);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _sharky.UnsubscribeOnAgroEvent(HandleExclamationParticle);
            _sharky.UnsubscribeOnWTFEvent(SpawnWtf);
            
            //Debug.Log("Clear Particales");
        }

        protected override void HandleSpawnMovementPartical()
        {
            if (_sharky != null 
                && Math.Abs(_sharky.Rb2D.velocity.x) >= 0.1f 
                && _sharky.CollisionInfo.IsGrounded
                && MoveSpawnTimer <= 0)
            {
                HandleSpawn(ParticleType.Move);
                MoveSpawnTimer = 0.25f;
            }
        }


        public void HandleExclamationParticle()
        {
            HandleSpawn(ParticleType.Exclamation);
            Debug.Log("Agro");
        }
        public void SpawnWtf() => HandleSpawn(ParticleType.Interrogation);
        
    }
}