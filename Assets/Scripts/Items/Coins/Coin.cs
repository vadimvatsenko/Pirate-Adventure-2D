using Controllers;
using Player;
using UnityEngine;

namespace Items.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinType coinType;
        [SerializeField] private int coinCost;
        [SerializeField] private GameObject coinDestroyVfx;
        private CoinsController _coinsController;

        private void Awake()
        {
            _coinsController = FindObjectOfType<EnterPoint>().CoinsController;
        }
        
        private void Collect()
        {
            _coinsController.AddCoins(coinCost);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            
            if (hero != null)
            {
                Collect();
                
                GameObject vfx = Instantiate(coinDestroyVfx, transform.position, Quaternion.identity);
            }
        }
    }
}
