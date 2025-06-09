using System;
using Components.HealthComponentFolder;
using Items.Coins;
using UnityEngine;


namespace Components
{
    public class CoinsComponent : MonoBehaviour
    {
        public event Action<int> OnCoinsAdd;
        public event Action OnCoinsRemove;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                OnCoinsAdd?.Invoke(coin.CoinCost);
            }
            
            HealthModifier healthModifier = collision.GetComponent<HealthModifier>();
            if (healthModifier != null)
            {
                if (healthModifier.WhatIsHealth == HealthModifierType.Damage)
                {
                    OnCoinsRemove?.Invoke();
                }
            }
        }
    }
}