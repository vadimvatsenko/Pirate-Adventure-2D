using Interfaces;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 offset;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var faced = target.GetComponent<IMovable>();
            Vector3 spawnPos = target.position;

            if (faced != null)
            {
                spawnPos += offset * -faced.FacingDirection;
            }

            GameObject spawnObj = Instantiate(prefab, spawnPos, target.rotation);
        }
    }
}