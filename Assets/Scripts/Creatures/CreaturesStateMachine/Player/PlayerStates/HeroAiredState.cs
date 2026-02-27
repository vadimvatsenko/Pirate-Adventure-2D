using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.PlayerStates
{
    public class HeroAiredState : HeroState
    {
        private bool _isAirborne;
        protected bool CanDoubleJump;
        
        public HeroAiredState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }
        public override void Update()
        {
            base.Update();
            UpdateAirBornStatus();
            
            if (Hr.XInput != 0)
            {
                Rb2D.velocity = new Vector2(Hr.XInput * (Hr.MovementSpeed * .8f), Rb2D.velocity.y);
            }
            
            if (CanDoubleJump && Hr.NewInputSet.Hero.Jump.triggered)
            {
                CanDoubleJump = false;
                StateMachine.ChangeState(Hr.DoubleJumpState);
            }
        }
        
        private void UpdateAirBornStatus() // 6 - переключатель состояния персонажа в воздухе  
        {  
            if (Hr.HeroCollision.IsGrounded && _isAirborne) HandleLanding();   
            if (!Hr.HeroCollision.IsGrounded && !_isAirborne) BecomeAirborn();  
        } 
     
        private void BecomeAirborn() // 8  
        {  
            _isAirborne = true;  
        }  
        private void HandleLanding() // 7  
        {  
            _isAirborne = false;  
            CanDoubleJump = true;  
        }  
    }
}