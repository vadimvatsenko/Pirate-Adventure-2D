using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    // настройки для нашего игрока
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int inventorySize;
        public int InventorySize => inventorySize;
    }
}