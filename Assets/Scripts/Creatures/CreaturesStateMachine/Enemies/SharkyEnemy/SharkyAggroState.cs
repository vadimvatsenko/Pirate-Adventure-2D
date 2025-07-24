using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAggroState : SharkyState
    {
        private float _aggroTimer = 0f;
        
        public SharkyAggroState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = Vector2.zero;
            Sharky.CallOnAgroEvent();
        }

        public override void Update()
        {
            base.Update();
            
            _aggroTimer += Time.deltaTime;
            if (_aggroTimer >= Sharky.AggroDuration)
            {
                OnAggroEnded();
                _aggroTimer = 0f;
            }
        }

        public void OnAggroEnded()
        {
            StateMachine.ChangeState(Sharky.BattleState);
        }
    }
}