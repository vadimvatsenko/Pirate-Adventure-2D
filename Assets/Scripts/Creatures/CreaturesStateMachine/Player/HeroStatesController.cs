using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroStatesController
    {
        private readonly Creature _creature;
        private readonly CreatureStateMachine _stateMachine;
        private readonly NewInputSet _newInputSet;
        private readonly GameSession _gameSession;
        private readonly Animator _animator;

        private float _throwPressedTime;
        private bool _isSubscribed;

        private bool _isThrowing;
        private int _pendingThrows;
        
        public HeroStatesController(Creature creature, 
            CreatureStateMachine stateMachine, 
            NewInputSet newInputSet,
            GameSession gameSession,
            Animator animator)
        {
            _creature = creature;
            _stateMachine = stateMachine;
            _newInputSet  = newInputSet;
            _gameSession = gameSession;
            _animator = animator;
            
            if (!_isSubscribed && (newInputSet != null))
            {
                _newInputSet.Hero.Attack.started += OnAttackStarted;
                
                _newInputSet.Hero.Throw.started += OnThrowBtnStarted;
                _newInputSet.Hero.Throw.canceled += OnthrowBtnCanceled;
                
                _creature.ThrowState.OnExitEvent += TryStartNextThrow;
                
                _isSubscribed = true;
            }
        }

        ~HeroStatesController()
        {
            if (_isSubscribed)
            {
                _newInputSet.Hero.Throw.started -= OnThrowBtnStarted;
                _newInputSet.Hero.Throw.canceled -= OnthrowBtnCanceled;
                
                _newInputSet.Hero.Attack.started -= OnAttackStarted;
                
                _creature.ThrowState.OnExitEvent -= TryStartNextThrow;
                
                _isSubscribed = false;
            }
        }

        private void OnThrowBtnStarted(InputAction.CallbackContext ctx)
        {
            if(_gameSession.PlayerData.swords <= 1) return;
            
            _throwPressedTime = (float)ctx.time;
            _animator.SetFloat(AnimatorHashes.ThrowTrigger, 0f);
            _stateMachine.ChangeState(_creature.ThrowState);
            
        }
        private void OnthrowBtnCanceled(InputAction.CallbackContext ctx)
        {
            float holdTime = (float)ctx.time - _throwPressedTime;
            
            // 1..3 броска по длительности удержания
            int wanted = Mathf.Clamp(Mathf.RoundToInt(holdTime), 1, 3);
            
            // сколько могу выбросить учитывая остаток
            int can = Mathf.Min(wanted, _gameSession.PlayerData.swords -1);
            
            if (can <= 0) return;
            
            _pendingThrows = can;
            
            _animator.SetFloat(AnimatorHashes.ThrowTrigger, 1f);
        }


        private void TryStartNextThrow()
        {
            if (_pendingThrows > 0)
            {
                _pendingThrows--;
                _gameSession.PlayerData.swords--;
                _stateMachine.ChangeState(_creature.ThrowState);
            }
        }

        private void OnAttackStarted(InputAction.CallbackContext ctx)
        {
            if (_gameSession.PlayerData.isArmed)
            {
                _stateMachine.ChangeState(_creature.AttackState);
            } 
        }
    }
}