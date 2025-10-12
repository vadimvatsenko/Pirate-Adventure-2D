using System.Collections;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroHitState : HeroState
    {
        private bool _isHited;
        private Coroutine _hitCoroutine;
        
        public HeroHitState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            ReciveHit(Hr.FinalHit, Hr.FinalHitDuration);
        }

        public override void Update()
        {
            base.Update();
            
            if (CollisionInfo.IsGrounded && !_isHited)
            {
                StateMachine.ChangeState(Hr.IdleState);
            }
        }

        public void ReciveHit(Vector2 hitDirection, float duration)
        {
            if (_hitCoroutine != null)
            {
                Hr.StopCoroutine(_hitCoroutine); // вызов курутины из скрипта Mono (через Hr)
            }
            _hitCoroutine = Hr.StartCoroutine(HitCoroutine(hitDirection, duration));
        }

        private IEnumerator HitCoroutine(Vector2 hirDirection, float duration)
        {
            _isHited = true;
            Hr.NewInputSet.Disable();
            Rb2D.velocity = new Vector2(hirDirection.x, hirDirection.y);
            yield return new WaitForSeconds(duration);
            //Rb2D.velocity = Vector2.zero;
            _isHited = false;
            Hr.NewInputSet.Enable();
        }
    }
}