using Creatures.CreaturesStateMachine.Player;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using UnityEngine;

namespace Components.EnterTriggerComponents
{
    public class InventoryItemsTriggerComponent : AnimationTriggerComponent
    {
        private bool _inventoryIsFull;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Hero hero = other.gameObject.GetComponent<Hero>();
                
            _inventoryIsFull = hero.GameSess.PlayerData.InventoryData.InventoryItem.Count >= DefFacade.Instance.PlayerDef.InventorySize;

            if (_inventoryIsFull)
            {
                Debug.Log("Inventory Full");
            }
            else
            {
                base.OnTriggerEnter2D(other);
            }
            
        }
    }
}