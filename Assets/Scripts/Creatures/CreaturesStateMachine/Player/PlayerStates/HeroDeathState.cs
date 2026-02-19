using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroDeathState : HeroState
    {

        public HeroDeathState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Hr.NewInputSet.Disable();
            StateMachine.SwitchOffStateMachine();
            Hr.CallOnDeathEvent();
            Hr.GameSess.ReloadLevel();
        }
    }
}