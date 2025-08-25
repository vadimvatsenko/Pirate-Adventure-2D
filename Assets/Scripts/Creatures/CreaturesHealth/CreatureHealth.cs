using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.EnemyStates;
using Creatures.CreaturesVFX;
using Creatures.Interfaces;
using UnityEngine;
using UnityEngine.TextCore;

namespace Creatures.CreaturesHealth
{
    public class CreatureHealth : MonoBehaviour
    {
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected bool isDead;
        
        private CreatureVFX _creaturesVFX;
        private Creature _creature;
        

        protected virtual void Awake()
        {
            _creaturesVFX = GetComponent<CreatureVFX>();
            _creature = GetComponentInParent<Creature>();
        }
        public virtual void TakeDamage(float damage, Transform attacker)
        {
            if(isDead) return;
            
            Vector2 hitDir = CaclulateHitDirection(attacker);
            
            // Тут нужно визвать состояние hit
            _creature.SetFinalHit(hitDir);
            _creature.StateMachine.ChangeState(_creature.HitState);
            
            _creaturesVFX.PlayOnDamageVFX();
            ReduceHealth(damage);
        }

        protected void ReduceHealth(float damage)
        {
            maxHealth -= damage;
            
            if (maxHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            _creature.StateMachine.ChangeState(_creature.DeathState);
            Debug.Log("Creature Die");
        }

        private Vector2 CaclulateHitDirection(Transform attacker)
        {
            int direction = transform.position.x > attacker.position.x ? 1 : -1;

            IFacingDirection facingCreature = attacker.GetComponent<IFacingDirection>();

            if (facingCreature != null)
            {
                int enemyFacingDirectionDirection = attacker.GetComponent<Enemy>().FacingDirection;
            
                if (enemyFacingDirectionDirection == _creature.FacingDirection)
                {
                    _creature.Flip();
                }
            }
            
            Vector2 hitPower = _creature.HitPower;
            hitPower.x *= direction;
            return hitPower;
        }
    }
}