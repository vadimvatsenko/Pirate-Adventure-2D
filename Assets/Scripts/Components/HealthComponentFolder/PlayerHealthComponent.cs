using System;
using Creatures;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.Hero;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        
        private Creature _creature;
        public UnityAction OnHealthChange;
        private GameSession _gameSession;
        
        public event Action OnDeath;

        private void Awake()
        {
            _creature = GetComponent<Hero>();
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        public void ApplyDamage(int damage)
        {
            _gameSession.PlayerData.health -= damage;
            
            if (_gameSession.PlayerData.health <= 0)
            {
                _gameSession.PlayerData.health = 0;
                OnDeath?.Invoke();
            }
        }

        public void ApplyHeal(int heal)
        {
            _gameSession.PlayerData.health += heal;
           
            if (_gameSession.PlayerData.health > _gameSession.PlayerData.maxHealth)
            {
                _gameSession.PlayerData.health = _gameSession.PlayerData.maxHealth;
            }
            
            OnHealthChange?.Invoke();
            onAddHealth?.Invoke();
        }

        public void AddHeart()
        {
            _gameSession.PlayerData.health++;
            _gameSession.PlayerData.maxHealth++;
            if (_gameSession.PlayerData.health >= _gameSession.PlayerData.maxTotalHearts)
            {
                _gameSession.PlayerData.health = _gameSession.PlayerData.maxTotalHearts;
                _gameSession.PlayerData.maxHealth = _gameSession.PlayerData.maxTotalHearts;
            }
            
            OnHealthChange?.Invoke();
        }
        private void SetHealthIfHeroDeath()
        {
            _gameSession.PlayerData.health = _gameSession.PlayerData.maxHealth;
        }
    }
}