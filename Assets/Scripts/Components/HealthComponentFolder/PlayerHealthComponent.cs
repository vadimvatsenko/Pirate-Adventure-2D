﻿using System;
using DefaultNamespace.Model;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;


namespace Components.HealthComponentFolder
{
    public class PlayerHealthComponent : MonoBehaviour, IHealthComponent
    {
        
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDie;
        
        private Player _player;
        public UnityAction OnHealthChange;
        private GameSession _gameSession;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _player.OnPlayerDeath += SetHealthIfPlayerDeath;
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void OnDisable()
        {
            _player.OnPlayerDeath += SetHealthIfPlayerDeath;
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
        private void SetHealthIfPlayerDeath()
        {
            _gameSession.PlayerData.health = _gameSession.PlayerData.maxHealth;
        }
    }
}