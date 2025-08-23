using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyRespawnState : EnemyState
    {
        public EnemyRespawnState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Respawn)))
            {
                if (Health.Health <= 0 && StateInfo.normalizedTime > 0.1f)
                {
                    StateMachine.ChangeState(Sharky.IdleState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}