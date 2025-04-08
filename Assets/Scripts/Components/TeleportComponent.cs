using UnityEngine;

namespace Components
{
    public class TeleportComponent :MonoBehaviour
    {
        [SerializeField] private Transform destTransform;

        public void Teleport(GameObject target)
        {
            target.transform.position = destTransform.position;
        }
    }
}