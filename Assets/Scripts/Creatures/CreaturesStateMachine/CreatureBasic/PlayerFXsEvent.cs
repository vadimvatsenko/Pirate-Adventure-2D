using System;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class PlayerFXsEvent : CreatureFXsEvent
    {
        [SerializeField] private UnityEvent onJump;
        [SerializeField] private UnityEvent onAttack;
        [SerializeField] private UnityEvent onDeath;

        private Hero _hero;
        
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _hero = GetComponentInParent<Hero>();
            _hero.JumpState.OnEnterEvent += JumpEffect;
            _hero.DeathState.OnEnterEvent += DeathEffect;
            _hero.HitState.OnEnterEvent += HitEffect;
        }

        private void OnDisable()
        {
            _hero.JumpState.OnEnterEvent -= JumpEffect;
            _hero.DeathState.OnEnterEvent -= DeathEffect;
            _hero.HitState.OnEnterEvent -= HitEffect;
        }

        private void JumpEffect() => onJump?.Invoke();
        private void DeathEffect() => onDeath?.Invoke();
        private void HitEffect() => onHit?.Invoke();
    }
}