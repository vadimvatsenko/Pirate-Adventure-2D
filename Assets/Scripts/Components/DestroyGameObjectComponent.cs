using UnityEngine;

namespace Components
{
    public class DestroyGameObjectComponent : MonoBehaviour
    {
        public void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }
    }
}