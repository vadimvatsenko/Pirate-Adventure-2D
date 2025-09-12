using System;
using UnityEngine;
using UnityEngine.Events;

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
        public int CoinCost => coinCost;
        
        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
    }
}
