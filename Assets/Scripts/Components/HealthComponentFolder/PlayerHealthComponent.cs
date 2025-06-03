﻿using DefaultNamespace.Model;
using UnityEngine;
using UnityEngine.Events;


namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : MonoBehaviour, IHealthComponent
    {
        
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDie;
        
        public UnityAction OnHealthChange;
        private GameSession _gameSession;
        
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _gameSession.PlayerData.health = _gameSession.PlayerData.maxHealth;
        }
        
        public void ApplyDamage(int damage)
        {
            _gameSession.PlayerData.health -= damage;
            
            onDamage?.Invoke();
            OnHealthChange?.Invoke();
            
            if (_gameSession.PlayerData.health <= 0)
            {
                _gameSession.PlayerData.health = 0;
                onDie?.Invoke();
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
    }
}