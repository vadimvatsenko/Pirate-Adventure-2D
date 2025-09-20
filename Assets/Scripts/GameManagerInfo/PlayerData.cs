using System;
using UnityEngine;

namespace GameManagerInfo
{
    [Serializable]
    public class PlayerData
    {
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
    }
}