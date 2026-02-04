using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemTrap : BasicCreature
    {
        protected override void Awake()
        {
            base.Awake();
            IdleState = new TrapIdleState(this, StateMachine, AnimatorHashes.Idle);
            HitState = new TotemHitState(this, StateMachine, AnimatorHashes.Hit);
            AttackState = new TotemAttackState(this, StateMachine, AnimatorHashes.Attack);
            PauseState = new TotemPauseState(this, StateMachine, AnimatorHashes.Idle);
            DeathState = new TotemDeathState(this, StateMachine, AnimatorHashes.Idle);
        }

        public void Start()
        {
            StateMachine.Initialize(IdleState);
        }
    }
}