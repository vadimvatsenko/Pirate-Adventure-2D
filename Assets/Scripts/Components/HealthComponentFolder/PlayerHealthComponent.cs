using System;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        
        private Hero _hero;
        public UnityAction OnHealthChange;
        private GameSession _gameSession;
        
        public event Action OnDeath;
        public event Action OnDamage;

        private void Awake()
        {
            _hero = GetComponent<Hero>();
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        public void ApplyDamage(int damage)
        {
            _gameSession.PlayerData.health -= damage;
            
            OnDamage?.Invoke();
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