using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyAttackState : EnemyState
    {
        private bool _damageDealt;
        private bool _attackEnded;
        
        public EnemyAttackState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = Vector2.zero;
            _damageDealt = false;
            _attackEnded = false;
        }

        public override void Update()
        {
            base.Update();

            // проверяем проигрывается ли анимация атаки
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)))
            {
                // наносим урон только один раз (например на 50% анимации)
                if(!_damageDealt && StateInfo.normalizedTime > 0.5f)
                {
                    Attack();
                    _damageDealt = true;
                }

                // конец анимации атаки — выходим в Idle, только один раз
                if(!_attackEnded && StateInfo.normalizedTime > 1.0f)
                {
                    _attackEnded = true;
                    StateMachine.ChangeState(Enemy.BattleState);
                }
            }
        }
        
        public void Attack()
        {
            /*if(!CollisionInfo.IsGrounded || _damageDealt) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                Debug.Log(go.name);
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(1);
                    _damageDealt = true;
                    return;
                }
            }*/
        }

        public override void Exit()
        {
            base.Exit();
            _damageDealt = false;
        }
    }
}