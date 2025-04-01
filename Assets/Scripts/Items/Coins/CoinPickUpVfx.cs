using UnityEngine;

namespace Items.Coins
{
    public class CoinPickUpVfx : MonoBehaviour
    {
        public void DestroyVfx() => Destroy(this.gameObject);
    }
}

