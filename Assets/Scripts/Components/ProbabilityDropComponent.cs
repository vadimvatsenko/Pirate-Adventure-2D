using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [Serializable]
    public class DropData
    {
        public GameObject DroppedObject;
        [Range(0f, 100f)] public float Probability;
    }

    [Serializable]
    public class DropEvent : UnityEvent<GameObject[]>
    {
        
    }
    // вероятность выпадения объектов
    public class ProbabilityDropComponent :MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private DropData[] dropData;
        [SerializeField] private DropEvent onDropCalculated;

        [ContextMenu("CalculateDrop")]
        public void CalculateDrop()
        {
            var itemsToDrop = new GameObject[count];
            var itemCount = 0;
            var total = dropData.Sum(x => x.Probability);
            var sortedDrop = dropData.OrderBy(x => x.Probability);

            while (itemCount < count)
            {
                var random = UnityEngine.Random.value * total;
                foreach (var dropData in sortedDrop)
                {
                    if (dropData.Probability >= random)
                    {
                        itemsToDrop[itemCount] = dropData.DroppedObject;
                        itemCount++;
                        break;
                    }
                }
            }
            
            onDropCalculated?.Invoke(itemsToDrop);
        }
    }
}