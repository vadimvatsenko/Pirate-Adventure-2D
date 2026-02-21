using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using Creatures.Interfaces;
using UnityEngine;


namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : BaseHealthComponent
    {
        private Hero _hero;
        private int _previousHealth;
        private int _maxHealth;
        
        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }
        private void Start()
        {
            Health = _hero.GameSess.PlayerData.health;
        }
        
        public override void TakeDamage(int damage, Transform damager)
        {
            base.TakeDamage(damage, damager);
            
            _hero.GameSess.PlayerData.health -= damage;
            Vector2 hitDir = CaclulateHitDirection(damage, damager);
            
            _hero.GameSess.PlayerData.health -= damage;
            
            float duration = CalculateDuration(damage);

            // Тут нужно визвать состояние hit
            
            _hero.SetFinalHitDuration(duration);
            _hero.SetFinalHit(hitDir);
            
            _hero.HandleHitState();

            ReduceHealth(damage); // важен порядок
        }
        
        private void ReduceHealth(int damage)
        {
            if(IsDead) return;
            
            _previousHealth = Health;
            Health -= damage;
            
            if (Health <= 0f)
            {
                _hero.HandleDeathState();
            }
        }
        
        private Vector2 CaclulateHitDirection(float damage, Transform attacker)
        {
            int direction = transform.position.x > attacker.position.x ? 1 : -1;

            IFacingDirection facingCreature = attacker.GetComponent<IFacingDirection>();

            if (facingCreature != null)
            {
                int enemyFacingDirectionDirection = attacker.GetComponent<IFacingDirection>().FacingDirection;
                
                if (enemyFacingDirectionDirection == _hero.FacingDirection)
                {
                    _hero.Flip();
                }
            }
            
            Vector2 hitPower = IsHeavyDamage(damage) ? _hero.HeavyHitPower : _hero.HitPower;

            hitPower.x *= direction;
            return hitPower;
        }

        private float CalculateDuration(float damage) =>
            IsHeavyDamage(damage) ? _hero.HeavyHitDuration : _hero.HitDuration;

        private bool IsHeavyDamage(float damage) => 
            damage / _maxHealth > _hero.HeavyDamageThreshold;
        
    }
}