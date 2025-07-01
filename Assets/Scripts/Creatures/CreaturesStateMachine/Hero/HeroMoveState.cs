using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroMoveState : HeroGroundState
    {
        private readonly Hero _hero;
        public HeroMoveState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
            _hero = hero;
        }

        public override void Enter()
        {
            base.Enter();
            _hero.Rb2D.velocity = new Vector2(Hero.XInput * Hero.MovementSpeed, Hero.Rb2D.velocity.y);
        }

        public override void Update()
        {
            base.Update();
            
            if (_hero.XInput == 0)
            {
                _hero.StateMachine.ChangeState(_hero.HeroIdleState);
            }
            _hero.Rb2D.velocity = new Vector2(Hero.XInput * Hero.MovementSpeed, Hero.Rb2D.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}