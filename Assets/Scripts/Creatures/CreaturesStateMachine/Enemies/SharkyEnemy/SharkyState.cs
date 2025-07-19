using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyState : CreatureState
    {
        protected Sharky EnemySharky;
        private readonly int _animName;
        
        public SharkyState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
            EnemySharky = enemySharky;
            _animName = animBoolName;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(AnimatorHashes.GetName(_animName));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}