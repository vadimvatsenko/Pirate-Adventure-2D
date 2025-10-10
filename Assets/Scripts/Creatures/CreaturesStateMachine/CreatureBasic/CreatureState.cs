using System;
using Components.HealthComponentFolder;
using Creatures.CreaturesCollisions;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureState : ICreatureState
    {
        protected Creature Creature;
        
        protected CreatureStateMachine StateMachine;
        protected readonly Rigidbody2D Rb2D;
        protected Collider2D C2D;
        
        protected readonly CreatureCollisionInfo CollisionInfo;
        protected readonly Animator AnimContr;
        protected readonly HealthComponent Health;
        public AnimatorStateInfo StateInfo { get; protected set; }

        public int AnimBoolName { get; private set; }
        public event Action OnEnterEvent; // вход в анимацию
        public event Action OnExitEvent; // выход с анимации

        public CreatureState(Creature creature, CreatureStateMachine stateMachine, int animBoolName)
        {
            this.Creature = creature;
            this.StateMachine = stateMachine;
            this.AnimBoolName = animBoolName;

            if (Creature != null)
            {
                this.Rb2D = creature.Rb2D;
                this.C2D = creature.C2D;
                AnimContr = creature.GetComponentInChildren<Animator>();
                this.CollisionInfo = creature.CollisionInfo;
                Health = Creature.GetComponent<HealthComponent>();
            }
        }
        
        public virtual void Enter()
        {
            AnimContr.SetBool(AnimBoolName, true); // вход
            OnEnterEvent?.Invoke();
        }

        public virtual void Update()
        {
            StateInfo = AnimContr.GetCurrentAnimatorStateInfo(0);
           //Debug.Log($"{Creature.name} in {AnimatorHashes.GetName(_animBoolName)}");
        }

        public virtual void Exit()
        {
            OnExitEvent?.Invoke();
            AnimContr.SetBool(AnimBoolName, false); // выход
        }
    }
}