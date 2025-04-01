using UnityEngine;

namespace Items.Coins
{
    public class CoinPickUpVfx : MonoBehaviour
    {
        public void DestroyVfx()
        {
            Debug.Log("Destroying VFX");
            Destroy(gameObject);
        }
    }
}

