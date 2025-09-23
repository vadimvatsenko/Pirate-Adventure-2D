using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyRespawnState : EnemyState
    {
        public EnemyRespawnState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
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
                    StateMachine.ChangeState(En.IdleState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}