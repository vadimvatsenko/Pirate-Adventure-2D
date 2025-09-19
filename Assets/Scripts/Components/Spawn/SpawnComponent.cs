using Creatures.Interfaces;
using UnityEngine;

namespace Components.Spawn
{
    public class SpawnComponent : MonoBehaviour
    {
        // Позиция где будем отображать партикл
        [SerializeField] private Transform target;
        // Префаб партикла
        [SerializeField] private GameObject prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            Vector3 spawnPos = transform.position;
            
            GameObject spawnObj = Instantiate(prefab, spawnPos, target.rotation);
            
            spawnObj.SetActive(true); // +++
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}