using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroFallState : HeroAiredState
    {
        private float _startFallY;

        private float _bufferJumpWindow;
        private float _bufferJumpActivated;
        
        
        public HeroFallState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            _bufferJumpWindow = hr.BufferJumpWindow;
            _bufferJumpActivated = hr.BufferJumpActivated;
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

            if (Hr.NewInputSet.Hero.Jump.triggered)
            {
                _bufferJumpActivated = Time.time;
            }

            if (CollisionInfo.IsGrounded)
            {
                AttemtBufferJump();
            }
            
            if (CollisionInfo.IsGrounded && Rb2D.velocity.y <= 0.1f)
            {
                float landedY = Hr.transform.position.y;
                float fallHeight = _startFallY - landedY;
                
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
        
        private void AttemtBufferJump()   
        {  
            if (Time.time < _bufferJumpActivated + _bufferJumpWindow)  
            {            
                _bufferJumpActivated = 0; // Сбрасываем буфер  
                StateMachine.ChangeState(Hr.JumpState);
            }    
        }
    }
}