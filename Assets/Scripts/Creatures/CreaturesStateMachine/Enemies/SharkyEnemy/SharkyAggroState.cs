using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAggroState : SharkyGroundedState
    {
        private float _aggroTimer = 0f;
        private float _aggroDuration;
        private bool _isAggroing;
        
        public SharkyAggroState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
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
            if (AnimatorContr.GetCurrentAnimatorStateInfo(0).IsName("Sharky_AGGRO") &&
                AnimatorContr.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                OnAggroEnded();
            }
        }

        public void OnAggroEnded()
        {
            StateMachine.ChangeState(Sharky.BattleState);
        }
    }
}