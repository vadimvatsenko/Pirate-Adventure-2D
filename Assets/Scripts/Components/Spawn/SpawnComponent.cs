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
        // Здвиг партикла
        [SerializeField] private Vector3 offset;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            // почему так? Это интерфейс контракт которого говорит, что у объекта будет направление движения
            var faced = target.GetComponent<IMovable>();
            //Vector3 spawnPos = target.position;
            Vector3 spawnPos = transform.position;
            
            if (faced != null)
            {
                //spawnPos += offset * -faced.FacingDirection;
                spawnPos += new Vector3(-faced.FacingDirection * offset.x, offset.y, offset.z);
            }
            else
            {
                spawnPos += offset;
            }

            GameObject spawnObj = Instantiate(prefab, spawnPos, target.rotation);
            spawnObj.SetActive(true); // +++
        }
    }
}