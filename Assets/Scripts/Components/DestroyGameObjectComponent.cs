using UnityEngine;

namespace Components
{
    public class DestroyGameObjectComponent : MonoBehaviour
    {
        public void DestroyGameObject()
        {
            Debug.Log("DestroyGameObject");
            Destroy(gameObject);
        }
    }
}