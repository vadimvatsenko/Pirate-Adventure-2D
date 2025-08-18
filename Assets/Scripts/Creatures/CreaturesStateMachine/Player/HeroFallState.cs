using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroFallState : HeroAiredState
    {
        private float _startFallY;
        
        public HeroFallState(Player.Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // фиксируем позицию по Y во время падения
            _startFallY = Hr.transform.position.y;
        }

        public override void Update()
        {
            base.Update();
            
            if (CollisionInfo.IsGrounded && Rb2D.velocity.y <= 0.1f)
            {
                float landedY = Hr.transform.position.y;
                float fallHeight = _startFallY - landedY;

                //Debug.Log($"Fall Height = {fallHeight}");

                if (fallHeight > 5f)
                {
                    StateMachine.ChangeState(Hr.DeathState);
                }
                else
                {
                    StateMachine.ChangeState(Hr.IdleState);
                }
            }
        }
    }
}