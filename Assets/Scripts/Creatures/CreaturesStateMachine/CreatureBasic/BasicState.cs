using System;
using Components.HealthComponentFolder;
using Creatures.CreaturesCollisions;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class BasicState : ICreatureState
    {
        protected BasicCreature Creature;
        
        protected BasicStateMachine StateMachine;
        protected readonly Rigidbody2D Rb2D;
        protected Collider2D C2D;
        
        protected readonly BasicCollisionInfo CollisionInfo;
        protected readonly Animator AnimContr;
        protected readonly HealthComponent Health;
        public AnimatorStateInfo StateInfo { get; protected set; }

        public int AnimBoolName { get; private set; }
        public event Action OnEnterEvent; // вход в анимацию
        public event Action OnExitEvent; // выход с анимации

        public BasicState(BasicCreature creature, BasicStateMachine stateMachine, int animBoolName)
        {
            this.Creature = creature;
            this.StateMachine = stateMachine;
            this.AnimBoolName = animBoolName;

            if (Creature != null)
            {
                this.Rb2D = creature.Rb2D;
                this.C2D = creature.C2D;
                AnimContr = creature.GetComponentInChildren<Animator>();
                this.CollisionInfo = creature.GetComponent<BasicCollisionInfo>();
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