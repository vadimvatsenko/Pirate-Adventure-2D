using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAttackState : SharkyState
    {
        public SharkyAttackState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            /*base.Update();
            
            AnimatorStateInfo stateInfo = Sharky.AnimController.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && stateInfo.normalizedTime >= 1f)
            {
                Sharky.StateMachine.ChangeState(Sharky.SharkyIdleState);
                Debug.Log("End Attack");
            }*/
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}