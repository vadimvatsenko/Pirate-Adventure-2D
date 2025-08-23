using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyDeathState : EnemyState
    {
        private float _deathTimer;
        private readonly float DeathDelay = 2f;
        private Vector2 _initialSize;
        
        public EnemyDeathState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            _deathTimer = 0f;
            _initialSize = C2D.bounds.size;
        }

        public override void Enter()
        {
            base.Enter();
            //Rb2D.velocity = new Vector2(2 * -Sharky.FacingDirection, 2);
            //Rb2D.AddForce(new Vector2(2 * -Sharky.FacingDirection, 3), ForceMode2D.Impulse);
            Health.enabled = false;
            _deathTimer = 0f;
        }

        public override void Update()
        {
            base.Update();
            
            _deathTimer += Time.deltaTime;

            float t = Mathf.Clamp01(_deathTimer / DeathDelay);
            
            //Vector2 newSize = Vector2.Lerp(_initialSize, Vector2.zero, t);
            Vector2 newSize = Vector2.Lerp(_initialSize, Vector2.one, t);

            CapsuleCollider2D capsule = C2D as CapsuleCollider2D;
            
            capsule.size = newSize; // уменьшаем размер коллайдера*/
            
            if (newSize.x <= 0 && newSize.y <= 0)
                Sharky.DestroySelf();
            
            /*if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Death)))
            {
                if (Health.Health <= 0 && StateInfo.normalizedTime > 0.1f)
                {
                    StateMachine.ChangeState(Sharky.RespawnState);
                }
            }*/
        }
    }
}