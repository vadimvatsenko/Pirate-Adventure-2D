using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemTrap : BasicCreature
    {
        private TotemCollisionInfo _totemCollisionInfo;
        protected override void Awake()
        {
            base.Awake();
            IdleState = new TrapIdleState(this, StateMachine, AnimatorHashes.Idle);
            HitState = new TotemHitState(this, StateMachine, AnimatorHashes.Hit);
            AttackState = new TotemAttackState(this, StateMachine, AnimatorHashes.Attack);
            PauseState = new TotemPauseState(this, StateMachine, AnimatorHashes.Idle);
            DeathState = new TotemDeathState(this, StateMachine, AnimatorHashes.Idle);
        }

        public void Start()
        {
            StateMachine.Initialize(IdleState);
            _totemCollisionInfo = GetComponent<TotemCollisionInfo>();
        }

        protected override void Update()
        {
            base.Update();

            if (_totemCollisionInfo != null)
            {
                _totemCollisionInfo.HeroDetection();
            }
            
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Hero hero = other.GetComponent<Hero>();

            if (hero != null)
            {
                Flip();
            }

            /*if (hero.transform.position.x > this.transform.position.x)
            {
                Debug.Log(hero.transform.position.x);
            }*/
        }
    }
}