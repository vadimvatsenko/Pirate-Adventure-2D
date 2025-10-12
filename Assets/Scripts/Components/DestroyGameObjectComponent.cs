using UnityEngine;

namespace Components
{
    public class DestroyGameObjectComponent : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 0f;
        public void DestroyGameObject() => Destroy(this.gameObject);
    }
}