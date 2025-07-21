using System;
using Creatures.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class CreatureState : ICreatureState
    {
        protected Creature Creature;
        protected CreatureStateMachine StateMachine;
        protected int _animBoolName;
        public event Action OnEnterEvent; // вход в анимацию
        public event Action OnExitEvent; // выход с анимации

        public CreatureState(Creature creature, CreatureStateMachine stateMachine, int animBoolName)
        {
            this.Creature = creature;
            this.StateMachine = stateMachine;
            this._animBoolName = animBoolName;
        }
        
        public virtual void Enter()
        {
            Creature.AnimController.SetBool(_animBoolName, true); // вход
            OnEnterEvent?.Invoke();
        }

        public virtual void Update()
        {
           //Debug.Log($"{Creature.name} in {AnimatorHashes.GetName(_animBoolName)}");
        }

        public virtual void Exit()
        {
            Creature.AnimController.SetBool(_animBoolName, false); // выход
            OnExitEvent?.Invoke();
        }
    }
}