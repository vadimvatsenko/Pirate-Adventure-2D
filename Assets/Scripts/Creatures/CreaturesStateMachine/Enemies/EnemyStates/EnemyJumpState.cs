using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyJumpState : EnemyAiredState
    {
        public EnemyJumpState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(Rb2D.velocity);
            Rb2D.velocity = new Vector2(2.5f, 3f);
            Debug.Log(Rb2D.velocity);
            En.CallOnJumpEvent();
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