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
            if (gameObjectTag.Equals(other.gameObject.tag))
            {
                var gamesess = other.gameObject.GetComponent<Hero>().GameSess;
               
                if (gamesess == null) return;
                
                _inventoryIsFull = gamesess.PlayerData.InventoryData.InventoryItem.Count >= DefFacade.Instance.PlayerDef.InventorySize;

                if (_inventoryIsFull)
                {
                    return;
                }
                else
                {
                    base.OnTriggerEnter2D(other);
                }
            }
        }
    }
}