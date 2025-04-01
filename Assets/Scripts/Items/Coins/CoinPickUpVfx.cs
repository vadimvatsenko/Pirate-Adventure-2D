using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Coins
{
    public class CoinPickUpVfx : MonoBehaviour
    {
        public void DestroyVfx() => Destroy(this.gameObject);
    }
}

