using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyAggroState : EnemyState
    {
        public EnemyAggroState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName)
            : base(enemy, stateMachine, animBoolName)
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

            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Aggro)) && StateInfo.normalizedTime > 1.0f)
            {
                Enemy.CallOnAgroEvent(); // VFX Agro
                StateMachine.ChangeState(Enemy.BattleState);
            }
        }
    }
}