using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAggroState : SharkyGroundedState
    {
        private float _aggroTimer = 0f;
        private float _aggroDuration;
        private bool _isAggroing;
        
        public SharkyAggroState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _aggroDuration = EnemySharky.AggroDuration;
            _aggroTimer = 0f;
            _isAggroing = true;
        }

        public override void Update()
        {
            base.Update();
            
            ArrgoTimer();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void ArrgoTimer()
        {
            _aggroTimer += Time.deltaTime;
            if (_aggroTimer >= _aggroDuration)
            {
                _isAggroing = false;
            }
        }
    }
}