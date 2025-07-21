using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyState : CreatureState
    {
        protected readonly SharkyE Sharky;
        protected new readonly CreatureStateMachine StateMachine;
        protected readonly Rigidbody2D Rb2D;
        protected readonly Collider2D C2D;
        protected readonly SharkyCollisionInfo CollisionInfo;
        protected readonly Animator AnimatorContr;
        
        public SharkyState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            Sharky = sharky;
            StateMachine = stateMachine;

            if (Sharky != null)
            {
                Rb2D = Sharky.Rb2D;
                CollisionInfo = Sharky.SharkyCollisionInfo;
                C2D = Sharky.C2D;
                AnimatorContr = Sharky.AnimController;
            }
            _animBoolName = animBoolName;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(AnimatorHashes.GetName(_animBoolName));
        }
    }
}