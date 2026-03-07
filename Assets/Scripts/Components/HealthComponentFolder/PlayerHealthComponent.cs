using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using Creatures.Interfaces;
using GameManagerInfo;
using UnityEngine;


namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : BaseHealthComponent
    {
        private Hero _hero;
        
        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }

        private void Start()
        {
            Health = _hero.GameSess.PlayerData.maxHealth;
            _hero.GameSess.PlayerData.health = _hero.GameSess.PlayerData.maxHealth;
        }
        
        public override void TakeHeal(int heal)
        {
            Health += heal;
            
            if (health > _hero.GameSess.PlayerData.maxHealth)
            {
                health = _hero.GameSess.PlayerData.maxHealth;
            }
            
            _hero.GameSess.PlayerData.ChangeHealth(Health);
        }

        public override void TakeDamage(int damage, Transform damager)
        {
            if(IsDead) return;
            
            int tepmDamage = damage;
            
            _hero.GameSess.PlayerData.health -= damage;

            if (_hero.GameSess.PlayerData.health < damage)
                tepmDamage = 100;
            
            Vector2 hitDir = CaclulateHitDirection(tepmDamage, damager);
            
            _hero.GameSess.PlayerData.health -= damage;
            
            float duration = CalculateDuration(damage);

            // Тут нужно визвать состояние hit
            
            _hero.SetFinalHitDuration(duration);
            _hero.SetFinalHit(hitDir);
            
            _hero.HandleHitState();

            ReduceHealth(damage); // важен порядок
            onTakeDamage?.Invoke();

            if (Health <= 0)
            {
                IsDead = true;
                onDeath?.Invoke();
            }
        }
        
        private void ReduceHealth(int damage)
        {
            if(IsDead) return;
            
            Health -= damage;
            
            if (Health <= 0f)
            {
                _hero.HandleDeathState();
            }
            _hero.GameSess.PlayerData.ChangeHealth(Health);
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
            damage / _hero.GameSess.PlayerData.maxHealth > _hero.HeavyDamageThreshold;
        
    }
}