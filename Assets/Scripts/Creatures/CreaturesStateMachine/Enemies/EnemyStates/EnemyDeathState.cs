using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyDeathState : EnemyState
    {
        private CapsuleCollider2D _deathCollider;
        
        private readonly Vector2 _startColSize;
        private readonly Vector2 _endColSize;
        
        private float _startTime;
        private readonly float _duration;
        
        public EnemyDeathState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
            _deathCollider = C2D as CapsuleCollider2D;
            _startColSize = C2D.bounds.size;
            _endColSize = new Vector2(C2D.bounds.size.x / 2, 0.0001f);

            _startTime = 0;
            _duration = 2f;
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.SwitchOffStateMachine();
            
        }

        public override void Update()
        {
            if (_startTime < _duration)
            {
                _startTime += Time.deltaTime;
                _deathCollider.size = Vector3.Lerp(_startColSize, _endColSize, _startTime / _duration);
                _deathCollider.offset = 
                    Vector3.Lerp(_deathCollider.offset, 
                                    new Vector3(_deathCollider.offset.x * -Creature.FacingDirection * 0.5f, _deathCollider.offset.x), 
                                    _startTime / _duration);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _startTime = 0f;
        }
    }
}