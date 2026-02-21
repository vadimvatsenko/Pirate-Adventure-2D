using System;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData inventoryData;
        public InventoryData InventoryData => inventoryData;
        
        [Header("IsArmed")]
        public bool isArmed;
        
        [Header("Health")]
        public int health;
        public int maxHealth;
        public int maxTotalHearts;

        public PlayerData Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }

        public void ChangeHealth(int health)
        {
            health = this.health;
        }
    }
}