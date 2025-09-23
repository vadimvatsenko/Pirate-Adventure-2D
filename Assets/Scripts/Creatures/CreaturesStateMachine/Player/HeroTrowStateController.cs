using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroTrowStateController : HeroState
    {
        public HeroTrowStateController(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
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