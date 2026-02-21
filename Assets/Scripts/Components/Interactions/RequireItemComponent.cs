using Creatures.CreaturesStateMachine.Player.Model.Data;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactions
{
    // для взаимодействие ключа 
    public class RequireItemComponent : MonoBehaviour
    {
        // можно выбрать много предметов для условия выполнения, например нам нужно 1 ключ и 5 монет для открытия двери
        [SerializeField] private InventoryItemData[] requiredItems;
        [SerializeField] private bool removeAfterUse;
        
        [SerializeField] private UnityEvent onSuccess;
        [SerializeField] private UnityEvent onFailure;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementMet = true;

            foreach (var requiredItem in requiredItems)
            {
                var numItems = session.PlayerData.InventoryData.Count(requiredItem.Id);

                if (numItems < requiredItem.Value)
                {
                    Debug.Log("False");
                    areAllRequirementMet = false;
                }
            }

            if (areAllRequirementMet)
            {
                if (removeAfterUse)
                {
                    foreach (var item in requiredItems)
                    {
                        session.PlayerData.InventoryData.Remove(item.Id, item.Value);
                    }
                }
                onSuccess.Invoke();
            }
            else
            {
                onFailure?.Invoke();
            }
        }
    }
}