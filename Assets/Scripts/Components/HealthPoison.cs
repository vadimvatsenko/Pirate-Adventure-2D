using UnityEngine;

namespace Components
{
    public class HealthPoison : MonoBehaviour
    {
        public void DestroyHealthPoison()
        {
            Destroy(this.gameObject);
        }
    }
}