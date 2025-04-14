using Controllers;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinType coinType;
        [SerializeField] private int coinCost;
        [SerializeField] private GameObject coinDestroyVfx;
        [SerializeField] private UnityEvent oncoinDestroyCoin;
        private CoinsController _coinsController;

        private void Awake()
        {
            _coinsController = FindObjectOfType<EnterPoint>().CoinsController;
        }
        
        private void Collect()
        {
            oncoinDestroyCoin?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player hero = collision.GetComponent<Player>();
            
            if (hero != null)
            {
                Collect();
                GameObject vfx = Instantiate(coinDestroyVfx, transform.position, Quaternion.identity);
            }
        }
    }
}
