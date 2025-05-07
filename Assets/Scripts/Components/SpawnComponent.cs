using Interfaces;
using UnityEngine;

namespace Components
{
    public class SpawnComponent : MonoBehaviour
    {
        // Позиция где будем отображать партикл
        [SerializeField] private Transform target;
        // Префаб партикла
        [SerializeField] private GameObject prefab;
        // Здвиг партикла
        [SerializeField] private Vector3 offset;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            // почему так? Это интерфейс контракт которого говорит, что у объекта будет направление движения
            var faced = target.GetComponent<IMovable>();
            //Vector3 spawnPos = target.position;
            Vector3 spawnPos = Vector3.zero;
            
            Debug.Log(faced.FacingDirection);

            if (faced != null)
            {
                spawnPos = transform.position + offset * -faced.FacingDirection;
            }
            /*else
            {
                Debug.LogWarning("No facing direction provided");
                spawnPos += offset;
            }*/

            GameObject spawnObj = Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}