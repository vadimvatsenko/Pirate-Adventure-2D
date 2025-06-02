using System;
using Items.Coins;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class CoinsComponent :MonoBehaviour
    {
        private int _score;
        public int Score => _score;
        
        public event Action<int> OnScoreChanged;

        private void Start()
        {
            _score = 0;
        }

        public void AddScore(int score)
        {
            _score += score;
            OnScoreChanged?.Invoke(_score);
        }

        public void RemoveScore(int score)
        {
            _score -= score;
            if (_score <= 0)
            {
                _score = 0;
            }
            OnScoreChanged?.Invoke(_score);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                AddScore(coin.CoinCost);
            }
        }
    }
}