using System;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryDataSerializable inventoryDataSerializable;
        
        [Header("Coins")]
        public int coins;
        [Header("IsArmed")]
        public bool isArmed;

        [Header("Swords")] 
        public int swords;
        [Header("Health")]
        public int health;
        public int maxHealth;
        public int maxTotalHearts;

        public PlayerData Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}