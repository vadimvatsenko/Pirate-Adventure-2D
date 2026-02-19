using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    [CreateAssetMenu(menuName = "Defs/Inventory/InventoryItemsDif", fileName = "InventoryItemsDif")]
    public class InventoryItemDefScriptable : ScriptableObject
    {
        [SerializeField] ItemDefSerializable[] items;

        public ItemDefSerializable GetItem(string id)
        {
            foreach (var itemDef in items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }
            return default;
        }
    }
}