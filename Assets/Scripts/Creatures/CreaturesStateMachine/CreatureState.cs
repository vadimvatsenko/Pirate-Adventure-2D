using Creatures.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class CreatureState : ICreatureState
    {
        protected Creature Creature;
        protected CreatureStateMachine StateMachine;
        private readonly int _animBoolName;

        public CreatureState(Creature creature, CreatureStateMachine stateMachine, int animBoolName)
        {
            this.Creature = creature;
            this.StateMachine = stateMachine;
            this._animBoolName = animBoolName;
        }
        
        public virtual void Enter()
        {
            Creature.AnimController.SetBool(_animBoolName, true); // вход
        }

        public virtual void Update()
        {
           //Debug.Log($"{Creature.name} in {AnimatorHashes.GetName(_animBoolName)}");
        }

        public virtual void Exit()
        {
            Creature.AnimController.SetBool(_animBoolName, false); // выход
        }
    }
}