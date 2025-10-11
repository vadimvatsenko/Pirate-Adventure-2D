using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Items.Traps.Totems
{
    public class TotemTrap : BasicCreature
    {
        protected override void Awake()
        {
            base.Awake();
            IdleState = new TrapIdleState(this, StateMachine, AnimatorHashes.Idle);
            HitState = new TotemHitState(this, StateMachine, AnimatorHashes.Hit);
            AttackState = new TotemAttackState(this, StateMachine, AnimatorHashes.Attack);
        }

        public void Start()
        {
            StateMachine.Initialize(IdleState);
        }
    }
}