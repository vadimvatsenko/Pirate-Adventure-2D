using UnityEngine;

namespace Items.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinType coinType;
        [SerializeField] private int coinCost;
        [SerializeField] private CoinsController coinsController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            
            if (hero != null)
            {
                coinsController.AddCoins(coinCost);
                Destroy(gameObject);
            }
        }
    }
}
