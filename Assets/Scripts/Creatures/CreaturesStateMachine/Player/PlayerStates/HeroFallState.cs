using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.PlayerStates
{
    public class HeroFallState : HeroAiredState
    {
        // буфер прыжка
        private readonly float _bufferJumpWindow;
        private float _bufferJumpActivated;
        // койот
        private readonly float _coyoteJumpWindow;
        private float _coyoteJumpActivated;
        
        // сметрельная высота
        private const float DEADLY_HEIGHT = 5f;
        
        private int _jumpCount;
        private float _startFallY;
        
        public HeroFallState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            _bufferJumpWindow = hr.BufferJumpWindow;
            _coyoteJumpWindow = hr.CoyoteJumpWindow;
        }

        public override void Enter()
        {
            base.Enter();
            // фиксируем позицию по Y во время падения
            _startFallY = Hr.transform.position.y;
            ActivateCoyoteJump();
        }

        public override void Update()
        {
            base.Update();

            bool jumpPressed = Hr.NewInputSet.Hero.Jump.triggered;
            
            if (jumpPressed)
            {
                ActivateBufferJump();
                
                bool coyoteJumpAvalible
                    = Time.time < _coyoteJumpActivated + _coyoteJumpWindow;
                
                if (coyoteJumpAvalible && StateMachine.PreviousState != Hr.JumpState)
                {
                    Hr.StateMachine.ChangeState(Hr.JumpState);
                    CancelCoyoteJump();
                }
            }
            
            if (Time.time < _bufferJumpActivated + _bufferJumpWindow 
                && CollisionInfo.IsGrounded)  
            {            
                CancelBufferJump();
                StateMachine.ChangeState(Hr.JumpState);  
            }    
            
            // проверка высоты падения
            if (CollisionInfo.IsGrounded && Rb2D.velocity.y <= 0.1f)
            {
                float landedY = Hr.transform.position.y;
                float fallHeight = _startFallY - landedY;
                
                Hr.JumpCounter = 0;
                
                if (fallHeight > DEADLY_HEIGHT)
                {
                    StateMachine.ChangeState(Hr.DeathState);
                }
                else
                {
                    StateMachine.ChangeState(Hr.IdleState);
                }
            }
        }
        
        private void ActivateCoyoteJump() => _coyoteJumpActivated = Time.time;
        private void CancelCoyoteJump() => _coyoteJumpActivated = Time.time - 1;
        private void ActivateBufferJump() => _bufferJumpActivated = Time.time;
        private void CancelBufferJump() => _bufferJumpActivated = Time.time - 1;
    }
}