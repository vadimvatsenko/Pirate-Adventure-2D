using UnityEngine;

namespace Components.Spawn
{
    public class SpawnComponent : MonoBehaviour
    {
        // Позиция где будем отображать партикл
        [SerializeField] protected Transform target;
        // Префаб партикла
        [SerializeField] protected GameObject prefab;
        
        [ContextMenu("Spawn")]
        public virtual void Spawn()
        {
            Vector3 spawnPos = transform.position;
            
            GameObject spawnObj = Instantiate(prefab, spawnPos, target.rotation);
            
            spawnObj.transform.localScale = target.lossyScale;
            
            spawnObj.SetActive(true);
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}