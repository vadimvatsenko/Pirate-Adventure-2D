using Creatures.CreaturesStateMachine.CreatureBasic;
using GameManagerInfo;
using UnityEditorInternal;
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

        private float _throwPressedTime;
        private bool _isSubscribed;

        private bool _isThrowing;
        private int _pendingThrows;
        
        public HeroStatesController(Creature creature, 
            CreatureStateMachine stateMachine, 
            NewInputSet newInputSet,
            GameSession gameSession)
        {
            _creature = creature;
            _stateMachine = stateMachine;
            _newInputSet  = newInputSet;
            _gameSession = gameSession;
            
            if (!_isSubscribed && (newInputSet != null))
            {
                _newInputSet.Hero.Attack.started += OnAttackStarted;
                
                _newInputSet.Hero.Throw.started += OnThrowStarted;
                _newInputSet.Hero.Throw.canceled += OnthrowCanceled;
                
                _creature.ThrowState.OnExitEvent += TryStartNextThrow;
                
                _isSubscribed = true;
            }
        }

        ~HeroStatesController()
        {
            if (_isSubscribed)
            {
                _newInputSet.Hero.Throw.started -= OnThrowStarted;
                _newInputSet.Hero.Throw.canceled -= OnthrowCanceled;
                
                _newInputSet.Hero.Attack.started -= OnAttackStarted;
                
                _creature.ThrowState.OnExitEvent -= TryStartNextThrow;
                
                _isSubscribed = false;
            }
        }
        
        private void OnThrowStarted(InputAction.CallbackContext ctx) 
            => _throwPressedTime = (float)ctx.time;
        private void OnthrowCanceled(InputAction.CallbackContext ctx)
        {
            if (_gameSession.PlayerData.swords <= 0 || _isThrowing) return;
            
            float holdTime = (float)ctx.time - _throwPressedTime;
            // 1..3 броска по длительности удержания
            int wanted = Mathf.Clamp(Mathf.RoundToInt(holdTime), 1, 3);
            
            // сколько могу выбросить учитывая остаток
            int can = Mathf.Min(wanted, _gameSession.PlayerData.swords -1);
            
            if (can <= 0) return;
            
            _pendingThrows = can;
            
            TryStartNextThrow();
        }
        
        
        private void TryStartNextThrow()
        {
            _isThrowing = true;
            
            if (_pendingThrows > 0)
            {
                _isThrowing = false;
                _pendingThrows--;
                _stateMachine.ChangeState(_creature.ThrowState);
            }
            else
            {
                _stateMachine.ChangeState(_creature.IdleState);
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