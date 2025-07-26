using Creatures.AnimationControllers;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAggroState : SharkyState
    {
        private float _aggroTimer = 0f;
        
        public SharkyAggroState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            Sharky.CallOnAgroEvent();
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Aggro)) && StateInfo.normalizedTime > 1.0f)
            {
                StateMachine.ChangeState(Sharky.BattleState);
            }
        }

        public void OnAggroEnded()
        {
            StateMachine.ChangeState(Sharky.BattleState);
        }
    }
}