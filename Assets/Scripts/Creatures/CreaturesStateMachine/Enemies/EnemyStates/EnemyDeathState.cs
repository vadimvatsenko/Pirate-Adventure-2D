using Components.HealthComponentFolder;
using Creatures.CreaturesHealth;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyDeathState : EnemyState
    {
        private readonly CapsuleCollider2D _deathCollider;
        
        private readonly Vector2 _startColSize;
        private readonly Vector2 _endColSize;
        
        private float _startTime;
        private readonly float _duration;
        
        public EnemyDeathState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
            _deathCollider = C2D as CapsuleCollider2D;
            _startColSize = C2D.bounds.size;
            //_endColSize = new Vector2(C2D.bounds.size.x, C2D.bounds.size.y);
            _endColSize = new Vector2(0.25f, 0.25f);

            _startTime = 0;
            _duration = 1f;
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.SwitchOffStateMachine();
            
            En.GetComponent<CreatureHealth>().enabled = false;
                
        }

        public override void Update()
        {
            if (_startTime < _duration)
            {
                _startTime += Time.deltaTime;
                float t = _startTime / _duration;
                _deathCollider.size = Vector3.Lerp(_startColSize, _endColSize, t);
                _deathCollider.offset = 
                    Vector3.Lerp(_deathCollider.offset, 
                                    new Vector3(-0.4f, -0.4f), t);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _startTime = 0f;
        }
    }
}