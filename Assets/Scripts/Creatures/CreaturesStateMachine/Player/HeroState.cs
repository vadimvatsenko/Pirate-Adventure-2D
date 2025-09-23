using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroState : CreatureState
    {
        protected readonly Hero Hr;
        private readonly PlayerHealthComponent _healthComponent;
        private bool _isSubscribed;
        
        //temp
        private int _throwSimpleIndex = 1;
        private int _throwComplexIndex = 3;
        
        public HeroState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            Hr = hr;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            if (!_isSubscribed)
            {
                Hr.NewInputSet.Hero.Throw.started += OnThrowStarted;
                Hr.NewInputSet.Hero.Attack.started += OnAttackStarted;
                _isSubscribed = true;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            
            if (_isSubscribed)
            {
                Hr.NewInputSet.Hero.Throw.started -= OnThrowStarted;
                Hr.NewInputSet.Hero.Attack.started -= OnAttackStarted;
                _isSubscribed = false;
            }
        }

        private void OnThrowStarted(InputAction.CallbackContext ctx)
        {
            if(Hr.GameSess.PlayerData.swords <= 1) return;

            StateMachine.ChangeState(Hr.ThrowState);

            Hr.GameSess.PlayerData.swords -= 1;
            
            Debug.Log(ctx.duration);
        }

        private void OnAttackStarted(InputAction.CallbackContext ctx)
        {
            if (Hr.GameSess.PlayerData.isArmed)
            {
                StateMachine.ChangeState(Hr.AttackState);
            } 
        }

        private void DeathHero() => Hr.StateMachine.ChangeState(Hr.DeathState);
        private void HitHero() => Hr.StateMachine.ChangeState(Hr.HitState);
        
    }
}