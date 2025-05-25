using System;
using Controllers;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Items.Coins
{
    [Serializable]
    public class ChangScoreEvent : UnityEvent<int>
    {
        public int score = 5;
    }
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinType coinType;
        [SerializeField] private int coinCost;
        [SerializeField] private ChangScoreEvent onAddCoin;
        
        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                onAddCoin?.Invoke(coinCost);
            }
        }
    }
}
