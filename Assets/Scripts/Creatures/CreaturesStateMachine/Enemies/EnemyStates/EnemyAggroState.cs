using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyAggroState : EnemyState
    {
        private float _aggroTimer = 0f;
        private readonly float _aggroDuration = 1.5f;
        
        public EnemyAggroState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            
            _aggroTimer += Time.deltaTime;
            if (_aggroTimer >= _aggroDuration)
            {
                _aggroTimer = 0f;
                if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Aggro)) 
                   && StateInfo.normalizedTime > 1.0f)
                {
                    Sharky.CallOnAgroEvent();
                    StateMachine.ChangeState(Sharky.BattleState);
                }
            }
        }
    }
}