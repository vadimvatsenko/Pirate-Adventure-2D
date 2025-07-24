using System;
using Creatures.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class CreatureState : ICreatureState
    {
        protected Creature Creature;
        
        protected CreatureStateMachine StateMachine;
        protected readonly Rigidbody2D Rb2D;
        protected Collider2D C2D;
        
        protected readonly CreatureCollisionInfo CollisionInfo;
        private readonly Animator _animContr;

        private readonly int _animBoolName;
        public event Action OnEnterEvent; // вход в анимацию
        public event Action OnExitEvent; // выход с анимации

        public CreatureState(Creature creature, CreatureStateMachine stateMachine, int animBoolName)
        {
            this.Creature = creature;
            this.StateMachine = stateMachine;
            this._animBoolName = animBoolName;

            if (Creature != null)
            {
                this.Rb2D = creature.Rb2D;
                this.C2D = creature.C2D;
                _animContr = creature.GetComponentInChildren<Animator>();
                this.CollisionInfo = creature.CollisionInfo;
            }
        }
        
        public virtual void Enter()
        {
            _animContr.SetBool(_animBoolName, true); // вход
            OnEnterEvent?.Invoke();
        }

        public virtual void Update()
        {
           //Debug.Log($"{Creature.name} in {AnimatorHashes.GetName(_animBoolName)}");
        }

        public virtual void Exit()
        {
            _animContr.SetBool(_animBoolName, false); // выход
            OnExitEvent?.Invoke();
        }
    }
}