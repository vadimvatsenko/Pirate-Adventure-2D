using Creatures.CreaturesStateMachine.Player;
using Creatures.CreaturesStateMachine.Player.Model.Data;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using UnityEngine;

namespace Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string id;
        [SerializeField] private int count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddInInventory(id, count);
            }
        }
    }
}