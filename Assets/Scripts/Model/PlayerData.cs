using System;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [Serializable]
    public class PlayerData
    {
        [Header("Coins")]
        public int coins;
        [Header("IsArmed")]
        public bool isArmed;
        [Header("Health")]
        public int health;
        public int maxHealth;
        public int maxTotalHearts;
    }
}